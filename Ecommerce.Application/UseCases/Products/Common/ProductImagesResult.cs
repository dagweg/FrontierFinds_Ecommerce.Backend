using Ecommerce.Application.UseCases.Images.Common;

namespace Ecommerce.Application.UseCases.Products.Common;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
public class ProductImagesResult
{
  public string? LeftImageUrl { get; set; }
  public string? RightImageUrl { get; set; }
  public string? FrontImageUrl { get; set; }
  public string? BackImageUrl { get; set; }
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
