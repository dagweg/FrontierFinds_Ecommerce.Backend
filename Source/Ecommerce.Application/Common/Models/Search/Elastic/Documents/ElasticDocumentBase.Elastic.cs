namespace Ecommerce.Application.Common.Models.Search.Elastic.Documents;

public abstract class ElasticDocumentBase : ElasticDocumentBase<string> { }

public abstract class ElasticDocumentBase<T>
{
  public T Id { get; set; } = default!;
}
