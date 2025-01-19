using Ecommerce.Application.UseCases.Images.Common;

namespace Ecommerce.Application.UseCases.Products.Common;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
public class ProductImagesResult
{
  public ImageResult? LeftImage { get; set; }
  public ImageResult? RightImage { get; set; }
  public ImageResult? FrontImage { get; set; }
  public ImageResult? BackImage { get; set; }
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
