using Ecommerce.Application.Common.Models.Search.Elastic;

namespace Ecommerce.Application.Common.Interfaces.Providers.Search.Elastic;

public interface IElasticSearch<TDocument>
  where TDocument : class
{
  Task<bool> IndexAsync(
    string indexName,
    TDocument document,
    CancellationToken cancellationToken = default
  );
  Task<bool> DeleteAsync(
    string indexName,
    string id,
    CancellationToken cancellationToken = default
  );
  Task<bool> UpdateAsync(
    string indexName,
    string id,
    TDocument document,
    CancellationToken cancellationToken = default
  );
  Task<TDocument?> GetAsync(
    string indexName,
    string id,
    CancellationToken cancellationToken = default
  );
  Task<IEnumerable<TDocument>> SearchAsync(
    ElasticSearchFilterParams searchParams,
    CancellationToken cancellationToken = default
  );
}
