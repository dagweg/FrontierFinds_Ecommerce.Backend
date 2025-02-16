using Ecommerce.Application.UseCases.Images.Common;
using Ecommerce.Domain.Common.Entities;

namespace Ecommerce.Application.UseCases.Products.Common;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
public class ProductResult
{
  public string ProductId { get; set; }
  public string ProductName { get; set; }
  public string ProductDescription { get; set; }
  public int StockQuantity { get; set; }
  public decimal PriceValue { get; set; }
  public string PriceCurrency { get; set; }
  public ProductImagesResult Images { get; set; }
  public List<TagResult>? Tags { get; set; }
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
