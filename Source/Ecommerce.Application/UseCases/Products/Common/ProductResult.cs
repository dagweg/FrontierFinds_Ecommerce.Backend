using Ecommerce.Application.UseCases.Images.Common;
using Ecommerce.Domain.Common.Entities;

namespace Ecommerce.Application.UseCases.Products.Common;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
public class ProductResult
{
  public required string ProductId { get; set; }
  public required string ProductName { get; set; }
  public required string Slug { get; set; }
  public required string ProductDescription { get; set; }
  public required int StockQuantity { get; set; }
  public required long PriceValueInCents { get; set; }
  public required string PriceCurrency { get; set; }
  public required decimal AverageRating { get; set; }
  public required ProductImagesResult Images { get; set; }
  public required List<TagResult>? Tags { get; set; }
  public required List<CategoryResult>? Categories { get; set; }
  public Guid SellerId { get; set; }
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
