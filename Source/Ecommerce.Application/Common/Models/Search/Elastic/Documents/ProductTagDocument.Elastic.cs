namespace Ecommerce.Application.Common.Models.Search.Elastic.Documents;

public class TagDocument : ElasticDocumentBase
{
  public string Name { get; set; } = default!;
}
