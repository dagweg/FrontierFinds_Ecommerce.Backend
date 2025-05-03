using Ecommerce.Application.Common.Models.Search.Elastic;
using Ecommerce.Application.Common.Models.Search.Elastic.Documents;

namespace Ecommerce.Application.Common.Interfaces.Providers.Search.Elastic;

public interface IElasticSearch
{
  Task<bool> IndexAsync<TDocument>(
    string indexName,
    TDocument document,
    CancellationToken cancellationToken = default
  )
    where TDocument : ElasticDocumentBase;
  Task<bool> DeleteAsync(
    string indexName,
    string id,
    CancellationToken cancellationToken = default
  );
  Task<bool> UpdateAsync(
    string indexName,
    string id,
    ElasticDocumentBase document,
    CancellationToken cancellationToken = default
  );
  Task<ElasticDocumentBase?> GetAsync(
    string indexName,
    string id,
    CancellationToken cancellationToken = default
  );
  Task<IEnumerable<TDocument>> SearchAsync<TDocument>(
    ElasticSearchFilterParams searchParams,
    CancellationToken cancellationToken = default
  )
    where TDocument : ElasticDocumentBase;

  Task<bool> IsReachableAsync();
}
