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

namespace Ecommerce.Infrastructure.Services.Providers.Search.Elastic;

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

      // Remove blocking ping check from constructor - connectivity will be checked when needed
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
    catch (Exception ex)
    {
      _logger.LogError(ex, "An unexpected error occurred during Elasticsearch client creation.");
      throw new InvalidOperationException("Failed to configure Elasticsearch client.", ex);
    }
  }

  public async Task<bool> DeleteAsync(
    string indexName,
    string id,
    CancellationToken cancellationToken = default
  )
  {
    try
    {
      var dr = await _elasticClient.DeleteAsync(
        new DeleteRequest(indexName, id),
        cancellationToken
      );
      return dr.IsSuccess();
    }
    catch (Exception ex)
    {
      _logger.LogWarning(
        ex,
        "Failed to delete document from Elasticsearch. Index: {IndexName}, Id: {Id}",
        indexName,
        id
      );
      return false;
    }
  }

  public async Task<ElasticDocumentBase?> GetAsync(
    string indexName,
    string id,
    CancellationToken cancellationToken = default
  )
  {
    try
    {
      var gr = await _elasticClient.GetAsync<ElasticDocumentBase>(
        new GetRequest(indexName, id),
        cancellationToken
      );

      if (!gr.IsSuccess())
      {
        _logger.LogWarning(
          "Elasticsearch Get failed: {Error}",
          gr.DebugInformation ?? gr.ElasticsearchServerError?.Error.ToString()
        );
        return null;
      }

      if (!gr.Found)
      {
        _logger.LogDebug("Document with ID {Id} not found in index {IndexName}.", id, indexName);
        return null;
      }

      return gr.Source;
    }
    catch (Exception ex)
    {
      _logger.LogWarning(
        ex,
        "Failed to get document from Elasticsearch. Index: {IndexName}, Id: {Id}",
        indexName,
        id
      );
      return null;
    }
  }

  public async Task<bool> IndexAsync<TDocument>(
    string indexName,
    TDocument document,
    CancellationToken cancellationToken = default
  )
    where TDocument : ElasticDocumentBase
  {
    try
    {
      var ind = await _elasticClient.IndexAsync(
        new IndexRequest<TDocument>(document, index: indexName),
        cancellationToken
      );
      if (!ind.IsSuccess())
      {
        _logger.LogWarning(
          "Failed to index document to Elasticsearch. Index: {IndexName}, Error: {Error}",
          indexName,
          ind.DebugInformation ?? ind.ElasticsearchServerError?.Error.ToString()
        );
      }

      return ind.IsSuccess();
    }
    catch (Exception ex)
    {
      _logger.LogWarning(
        ex,
        "Failed to index document to Elasticsearch. Index: {IndexName}",
        indexName
      );
      return false;
    }
  }

  public async Task<IEnumerable<TDocument>> SearchAsync<TDocument>(
    ElasticSearchFilterParams filterParams,
    CancellationToken cancellationToken = default
  )
    where TDocument : ElasticDocumentBase
  {
    try
    {
      var sr = await _elasticClient.SearchAsync<TDocument>(
        new SearchRequest(filterParams.Index) { Query = BuildElasticQuery(filterParams) },
        cancellationToken
      );
      if (sr.IsValidResponse)
      {
        _logger.LogInformation("Successfully fetched from Elasticsearch");
        var result = sr.Documents.AsQueryable().Paginate(filterParams.Pagination);
        return result;
      }
      else
      {
        _logger.LogWarning(
          "Error occurred fetching from Elasticsearch: {Error}",
          sr.DebugInformation ?? sr.ElasticsearchServerError?.Error.ToString()
        );
        return Enumerable.Empty<TDocument>();
      }
    }
    catch (Exception ex)
    {
      _logger.LogWarning(
        ex,
        "Failed to search documents in Elasticsearch. Index: {IndexName}",
        filterParams.Index
      );
      return Enumerable.Empty<TDocument>();
    }
  }

  private Query BuildElasticQuery(ElasticSearchFilterParams filterParams)
  {
    var queryContainer = new BoolQuery();
    queryContainer.Should = queryContainer.Should ?? [];
    queryContainer.Must = queryContainer.Must ?? [];
    queryContainer.MustNot = queryContainer.MustNot ?? [];
    queryContainer.Filter = queryContainer.Filter ?? [];

    bool hasFilterClauses = false;

    // Seller ID Filtering
    if (filterParams.SellerId != null)
    {
      var sellerIdTermQuery = new TermQuery(
        new Field(nameof(ProductDocument.SellerId).PascalToCamelCase())
      )
      {
        Value = FieldValue.String(filterParams.SellerId.Value.ToString()),
      };

      if (filterParams.SubjectFilter == SubjectFilter.SellerProductsOnly)
      {
        queryContainer.Filter.Add(sellerIdTermQuery);
        hasFilterClauses = true;
      }
      else if (filterParams.SubjectFilter == SubjectFilter.AllProductsWithoutSeller)
      {
        queryContainer.MustNot.Add(sellerIdTermQuery);
        hasFilterClauses = true;
      }
    }

    // Price Filtering
    if (filterParams.MinPriceValueInCents != null || filterParams.MaxPriceValueInCents != null)
    {
      var priceRangeQuery = new NumberRangeQuery(
        new Field(nameof(ProductDocument.PriceValueInCents).PascalToCamelCase())
      )
      {
        Gte = filterParams.MinPriceValueInCents.HasValue
          ? filterParams.MinPriceValueInCents.Value
          : null,
        Lte = filterParams.MaxPriceValueInCents.HasValue
          ? filterParams.MaxPriceValueInCents.Value
          : null,
      };

      if (priceRangeQuery.Gte != null || priceRangeQuery.Lte != null)
      {
        queryContainer.Filter.Add(priceRangeQuery);
        hasFilterClauses = true;
      }
    }

    // Category Filtering
    if (filterParams.CategoryIds != null && filterParams.CategoryIds.Any())
    {
      var categoryTermsQuery = new TermsQuery()
      {
        Field = new Field(
          nameof(ProductDocument.Categories).PascalToCamelCase()
            + "."
            + nameof(CategoryDocument.Id).PascalToCamelCase()
        ),
        Terms = new TermsQueryField(
          filterParams.CategoryIds.Select(id => FieldValue.Long(id)).ToList()
        ),
      };

      queryContainer.Filter = queryContainer.Filter ?? new List<Query>();
      queryContainer.Filter.Add(categoryTermsQuery);
      hasFilterClauses = true;
    }

    bool hasScoringClauses = false;

    // Search Term Handling
    if (!string.IsNullOrWhiteSpace(filterParams.SearchTerm))
    {
      var searchTerm = filterParams.SearchTerm.ToLowerInvariant().Trim();

      queryContainer.Must.Add(
        new MultiMatchQuery
        {
          Query = searchTerm,
          Fields = new[]
          {
            $"{nameof(ProductDocument.Name).PascalToCamelCase()}^2",
            nameof(ProductDocument.Description).PascalToCamelCase(),
          },
          Type = TextQueryType.BestFields,
          Fuzziness = new Fuzziness("AUTO"),
          Operator = Operator.Or,
          Analyzer = "standard",
        }
      );

      queryContainer.Should.Add(
        new WildcardQuery(new Field(nameof(ProductDocument.Name).PascalToCamelCase()))
        {
          Value = $"*{searchTerm}*",
          Boost = 1.0f,
        }
      );

      queryContainer.Should.Add(
        new WildcardQuery(new Field(nameof(ProductDocument.Description).PascalToCamelCase()))
        {
          Value = $"*{searchTerm}*",
          Boost = 0.5f,
        }
      );

      hasScoringClauses = true;
      queryContainer.MinimumShouldMatch = 1;
    }

    return hasFilterClauses || hasScoringClauses ? queryContainer : new MatchAllQuery();
  }

  public async Task<bool> UpdateAsync(
    string indexName,
    string id,
    ElasticDocumentBase document,
    CancellationToken cancellationToken = default
  )
  {
    try
    {
      var ur = await _elasticClient.UpdateAsync<ElasticDocumentBase, ElasticDocumentBase>(
        new UpdateRequest<ElasticDocumentBase, ElasticDocumentBase>(indexName, id)
        {
          Doc = document,
        },
        cancellationToken
      );
      if (!ur.IsSuccess())
      {
        _logger.LogWarning(
          "Failed to update document in Elasticsearch. Index: {IndexName}, Id: {Id}, Error: {Error}",
          indexName,
          id,
          ur.DebugInformation ?? ur.ElasticsearchServerError?.Error.ToString()
        );
      }
      return ur.IsSuccess();
    }
    catch (Exception ex)
    {
      _logger.LogWarning(
        ex,
        "Failed to update document in Elasticsearch. Index: {IndexName}, Id: {Id}",
        indexName,
        id
      );
      return false;
    }
  }

  public async Task<bool> IsReachableAsync()
  {
    try
    {
#pragma warning disable CS0618
      _logger.LogInformation("Attempting ping to Elasticsearch.");
      var pingResponse = await _elasticClient.PingAsync();
#pragma warning restore CS0618

      if (!pingResponse.IsValidResponse)
      {
        if (pingResponse.TryGetOriginalException(out var originalException))
        {
          _logger.LogError(originalException, "Elasticsearch ping original exception.");
        }
        if (!string.IsNullOrEmpty(pingResponse.DebugInformation))
        {
          _logger.LogError(
            $"Elasticsearch ping debug information: {pingResponse.DebugInformation}"
          );
        }
        _logger.LogError(
          $"Failed to connect to Elastic Cluster with URI: {_elasticSettings.ConnectionString}. Ensure it is running and accessible."
        );
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
    catch (Exception ex)
    {
      _logger.LogWarning(
        ex,
        "Failed to ping Elasticsearch. URI: {ConnectionString}",
        _elasticSettings.ConnectionString
      );
      return false;
    }
  }
}
