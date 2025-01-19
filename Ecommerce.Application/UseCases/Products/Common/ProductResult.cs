using Ecommerce.Application.UseCases.Images.Common;
using Ecommerce.Domain.Common.Entities;

namespace Ecommerce.Application.UseCases.Products.Common;

public class ProductResult
{
  public string ProductId { get; set; }
  public string ProductName { get; set; }
  public string ProductDescription { get; set; }
  public int StockQuantity { get; set; }
  public decimal PriceValue { get; set; }
  public string PriceCurrency { get; set; }
  public ImageResult Thumbnail { get; set; }
  public ProductImagesResult Images { get; set; }
  public List<TagResult>? Tags { get; set; }
}
