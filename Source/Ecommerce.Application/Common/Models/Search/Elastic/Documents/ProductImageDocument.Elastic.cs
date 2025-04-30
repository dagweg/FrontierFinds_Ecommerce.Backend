namespace Ecommerce.Application.Common.Models.Search.Elastic.Documents;

public class ProductImageDocument : ElasticDocumentBase
{
  public string Url { get; set; } = default!;
  public string ObjectIdentifier { get; set; } = default!;
}
