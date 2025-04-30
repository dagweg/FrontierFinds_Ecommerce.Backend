namespace Ecommerce.Application.Common.Models.Search.Elastic.Documents;

public class CategoryDocument : ElasticDocumentBase
{
  public string Name { get; set; } = default!;
  public string Slug { get; set; } = default!;
  public bool IsActive { get; set; } = true;
}
