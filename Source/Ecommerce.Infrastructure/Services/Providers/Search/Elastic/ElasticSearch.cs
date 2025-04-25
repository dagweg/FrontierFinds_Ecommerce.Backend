using System.Text.Json;
using Ecommerce.Application.Common.Interfaces.Providers.Search.Elastic;
using Ecommerce.Application.Common.Models.Search.Elastic;
using Elastic.Clients.Elasticsearch;
using Elastic.Clients.Elasticsearch.QueryDsl;

public class ElasticSearch<TDocument>(ElasticsearchClient elasticsearch) : IElasticSearch<TDocument>
  where TDocument : class
{
  public async Task<bool> DeleteAsync(
    string indexName,
    string id,
    CancellationToken cancellationToken = default
  )
  {
    var dr = await elasticsearch.DeleteAsync(
      new DeleteRequest() { Index = indexName, Id = id },
      cancellationToken
    );
    return dr.IsSuccess();
  }

  public async Task<TDocument?> GetAsync(
    string indexName,
    string id,
    CancellationToken cancellationToken = default
  )
  {
    var gr = await elasticsearch.GetAsync<TDocument>(
      new GetRequest() { Index = indexName, Id = id },
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

  public async Task<bool> IndexAsync(
    string indexName,
    TDocument document,
    CancellationToken cancellationToken = default
  )
  {
    var ind = await elasticsearch.IndexAsync(
      new IndexRequest<TDocument>() { Document = document, Index = indexName }
    );
    return ind.IsSuccess();
  }

  public async Task<IEnumerable<TDocument>> SearchAsync(
    ElasticSearchFilterParams filterParams,
    CancellationToken cancellationToken = default
  )
  {
    var sr = await elasticsearch.SearchAsync<TDocument>(
      new SearchRequest(filterParams.Index) { Query = new Query() { } },
      cancellationToken
    );
    if (sr != null && sr.IsSuccess())
      return sr.Documents;
    else
      return Enumerable.Empty<TDocument>();
  }

  public async Task<bool> UpdateAsync(
    string indexName,
    string id,
    TDocument document,
    CancellationToken cancellationToken = default
  )
  {
    var ur = await elasticsearch.UpdateAsync<TDocument, TDocument>(
      new UpdateRequest<TDocument, TDocument>()
      {
        Index = indexName,
        Id = id,
        Doc = document,
      }
    );
    return ur.IsSuccess();
  }
}
