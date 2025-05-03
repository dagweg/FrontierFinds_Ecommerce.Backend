using System.Text.Json;
using Castle.Core.Logging;
using Ecommerce.Application.Common.Extensions;
using Ecommerce.Application.Common.Interfaces.Providers.Search.Elastic;
using Ecommerce.Application.Common.Models.Search.Elastic;
using Ecommerce.Application.Common.Models.Search.Elastic.Documents;
using Ecommerce.Application.Common.Utilities;
using Ecommerce.Application.UseCases.Products.Queries.GetFilteredProducts;
using Ecommerce.Infrastructure.Services.Providers.Search.Elastic;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;
using Elastic.Clients.Elasticsearch.QueryRules;
using Elastic.Transport;
using Elastic.Transport.Products.Elasticsearch;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

public class ElasticSearch : IElasticSearch
{
  private readonly ElasticsearchClient _elasticClient;
  private readonly ElasticSettings _elasticSettings;
  private readonly ILogger<ElasticSearch> _logger;

  public ElasticSearch(IOptions<ElasticSettings> elasticSettings, ILogger<ElasticSearch> logger)
  {
    _logger = logger;

    try
    {
      _logger.LogInformation("Attempting to retrieve ElasticSettings.");
      _elasticSettings = elasticSettings.Value;
      _logger.LogInformation(
        $"Retrieved ElasticSettings. ConnectionString: {_elasticSettings.ConnectionString}, DefaultIndex: {_elasticSettings.DefaultIndex}, Username: {_elasticSettings.Username}."
      );

      if (string.IsNullOrEmpty(_elasticSettings.ConnectionString))
      {
        _logger.LogError("ElasticSettings ConnectionString is null or empty!");
        // You might want to throw a more specific exception here
        throw new InvalidOperationException("Elasticsearch connection string is not configured.");
      }

      _logger.LogInformation(
        $"Attempting to create ElasticsearchClient for URI: {_elasticSettings.ConnectionString}."
      );
      _elasticClient = new ElasticsearchClient(
        new ElasticsearchClientSettings(new Uri(_elasticSettings.ConnectionString))
          .DefaultIndex(_elasticSettings.DefaultIndex)
          .Authentication(
            new BasicAuthentication(_elasticSettings.Username!, _elasticSettings.Password)
          )
      );
      _logger.LogInformation("ElasticsearchClient created successfully.");

      Task.WaitAll(IsReachableAsync());
    }
    catch (UriFormatException uriEx)
    {
      _logger.LogError(
        uriEx,
        $"UriFormatException while creating ElasticsearchClient with ConnectionString: {_elasticSettings?.ConnectionString}. Check the format."
      );
      throw new InvalidOperationException(
        "Failed to configure Elasticsearch client due to invalid connection string URI.",
        uriEx
      );
    }
    catch (Exception ex) // Catch any other exceptions during client creation or ping
    {
      _logger.LogError(
        ex,
        "An unexpected error occurred during Elasticsearch client creation or ping."
      );
      throw new InvalidOperationException(
        "Failed to configure or connect to Elasticsearch client.",
        ex
      );
    }
  }

  public async Task<bool> DeleteAsync(
    string indexName,
    string id,
    CancellationToken cancellationToken = default
  )
  {
    var dr = await _elasticClient.DeleteAsync(new DeleteRequest(indexName, id), cancellationToken);
    return dr.IsSuccess();
  }

  public async Task<ElasticDocumentBase?> GetAsync(
    string indexName,
    string id,
    CancellationToken cancellationToken = default
  )
  {
    var gr = await _elasticClient.GetAsync<ElasticDocumentBase>(
      new GetRequest(indexName, id),
      cancellationToken
    );

    // Check if the request was successful AND if the document was found
    if (!gr.IsSuccess())
    {
      // Log the error from gr.DebugInformation or gr.ElasticsearchServerError
      Console.WriteLine(
        $"Elasticsearch Get failed: {gr.DebugInformation ?? gr.ElasticsearchServerError?.Error.ToString()}"
      );
      return null;
    }

    if (!gr.Found)
    {
      // Document not found, this is not an error, just means it doesn't exist
      Console.WriteLine($"Document with ID {id} not found in index {indexName}.");
      return null;
    }

    return gr.Source;
  }

  public async Task<bool> IndexAsync<TDocument>(
    string indexName,
    TDocument document,
    CancellationToken cancellationToken = default
  )
    where TDocument : ElasticDocumentBase
  {
    var ind = await _elasticClient.IndexAsync(
      new IndexRequest<TDocument>(document, index: indexName)
    );
    if (!ind.IsSuccess())
    {
      LogPretty.Log(ind.ElasticsearchServerError?.Error);
      if (ind.TryGetOriginalException(out var oex))
        LogPretty.Log(oex);
      else if (ind.TryGetElasticsearchServerError(out var ex))
        LogPretty.Log(oex);
      else
        LogPretty.Log(ind.DebugInformation);
    }

    return ind.IsSuccess();
  }

  public async Task<IEnumerable<TDocument>> SearchAsync<TDocument>(
    ElasticSearchFilterParams filterParams,
    CancellationToken cancellationToken = default
  )
    where TDocument : ElasticDocumentBase
  {
    var sr = await _elasticClient.SearchAsync<TDocument>(
      new SearchRequest(filterParams.Index) { Query = BuildElasticQuery(filterParams) },
      cancellationToken
    );
    if (sr.IsValidResponse)
    {
      // Log the response if needed
      _logger.LogInformation("Successfully fetched from elastic " + sr.DebugInformation);
      var result = sr.Documents.AsQueryable().Paginate(filterParams.Pagination);
      LogPretty.Log(result);
      return result;
    }
    else
    {
      // Log the error from sr.DebugInformation or sr.ElasticsearchServerError
      _logger.LogError(
        "Error occured fetching from elastic " + sr.DebugInformation
          ?? sr.ElasticsearchServerError?.Error.ToString()
      );
      return Enumerable.Empty<TDocument>();
    }
  }

  private Query BuildElasticQuery(ElasticSearchFilterParams filterParams)
  {
    var queryContainer = new BoolQuery();
    bool hasClauses = false;

    // --- Search Term Handling ---
    if (!string.IsNullOrWhiteSpace(filterParams.SearchTerm))
    {
      var searchTerm = filterParams.SearchTerm.ToLowerInvariant(); // Normalize

      queryContainer.Must = queryContainer.Must ?? new List<Query>();
      queryContainer.Must.Add(
        new MultiMatchQuery
        {
          Query = searchTerm,
          Fields = new[]
          {
            $"{nameof(ProductDocument.Name).PascalToCamelCase()}^2", // Boost name field
            nameof(ProductDocument.Description).PascalToCamelCase(),
          },
          Type = TextQueryType.BestFields,
          Fuzziness = new Fuzziness("AUTO"),
          Operator = Operator.Or, // Match any term within a field
          Analyzer = "standard",
        }
      );
      hasClauses = true;
    }

    // --- Seller ID Filtering ---
    if (filterParams.SellerId != null)
    {
      // Use TermQuery for exact match on keyword/numeric fields
      // Put it in the Filter context for non-scoring, performant filtering
      var sellerIdQuery = new TermQuery(
        new Field(nameof(ProductDocument.SellerId).PascalToCamelCase())
      )
      {
        Value = FieldValue.String(filterParams.SellerId.Value.ToString()),
      };

      if (filterParams.SubjectFilter == SubjectFilter.SellerProductsOnly)
      {
        queryContainer.Filter = queryContainer.Filter ?? new List<Query>();
        queryContainer.Filter.Add(sellerIdQuery);
        hasClauses = true;
      }
      else if (filterParams.SubjectFilter == SubjectFilter.AllProductsWithoutSeller)
      {
        queryContainer.MustNot = queryContainer.MustNot ?? new List<Query>();
        queryContainer.MustNot.Add(sellerIdQuery);
        hasClauses = true;
      }
      // Assuming SubjectFilter.AllProducts means no seller filter is applied
    }

    // If any clauses were added, return the BoolQuery, otherwise MatchAll
    return hasClauses ? queryContainer : new MatchAllQuery();
  }

  public async Task<bool> UpdateAsync(
    string indexName,
    string id,
    ElasticDocumentBase document,
    CancellationToken cancellationToken = default
  )
  {
    var ur = await _elasticClient.UpdateAsync<ElasticDocumentBase, ElasticDocumentBase>(
      new UpdateRequest<ElasticDocumentBase, ElasticDocumentBase>(indexName, id) { Doc = document }
    );
    return ur.IsSuccess();
  }

  public async Task<bool> IsReachableAsync()
  {
#pragma warning disable CS0618 // Type or member is obsolete
    _logger.LogInformation("Attempting synchronous ping to Elasticsearch.");
    var pingResponse = await _elasticClient.PingAsync();
#pragma warning restore CS0618 // Type or member is obsolete

    if (!pingResponse.IsValidResponse)
    {
      if (pingResponse.TryGetOriginalException(out var originalException))
      {
        _logger.LogError(originalException, "Elasticsearch ping original exception.");
      }
      if (!string.IsNullOrEmpty(pingResponse.DebugInformation))
      {
        _logger.LogError($"Elasticsearch ping debug information: {pingResponse.DebugInformation}");
      }
      _logger.LogError(
        $"Failed to connect to Elastic Cluster with URI: {_elasticSettings.ConnectionString}. Ensure it is running and accessible."
      );
      // Consider throwing an exception here if connection is critical for startup
      // throw new InvalidOperationException($"Failed to connect to Elasticsearch: {pingResponse.DebugInformation ?? pingResponse.OriginalException?.Message}");
      return false;
    }
    else
    {
      _logger.LogInformation(
        $"Successfully connected to Elastic Cluster with URI: {_elasticSettings.ConnectionString}."
      );
      return true;
    }
  }
}
