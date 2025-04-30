namespace Ecommerce.Application.Common.Models.Search.Elastic.Documents;

public class ProductImagesDocument : ElasticDocumentBase
{
  public required ProductImageDocument Thumbnail { get; set; }
  public ProductImageDocument? LeftImage { get; set; }
  public ProductImageDocument? RightImage { get; set; }
  public ProductImageDocument? FrontImage { get; set; }
  public ProductImageDocument? BackImage { get; set; }
  public ProductImageDocument? TopImage { get; set; }
  public ProductImageDocument? BottomImage { get; set; }
}
