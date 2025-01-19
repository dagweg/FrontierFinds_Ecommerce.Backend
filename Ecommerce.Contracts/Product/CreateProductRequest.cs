using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Ecommerce.Contracts.Image;

namespace Ecommerce.Contracts.Product;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
public class CreateProductRequest
{
  public string ProductName { get; init; }
  public string ProductDescription { get; init; }

  public int StockQuantity { get; init; }

  public decimal PriceValue { get; init; }

  public string PriceCurrency { get; init; }

  public CreateImageRequest Thumbnail { get; init; }
  public CreateImageRequest? LeftImage { get; init; }
  public CreateImageRequest? RightImage { get; init; }
  public CreateImageRequest? FrontImage { get; init; }
  public CreateImageRequest? BackImage { get; init; }
  public List<string>? Tags { get; init; }
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
