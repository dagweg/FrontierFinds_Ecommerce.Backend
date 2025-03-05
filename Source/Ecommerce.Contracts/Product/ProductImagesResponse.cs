using Ecommerce.Contracts.Image;

namespace Ecommerce.Contracts.Product;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
public class ProductImagesResponse
{
  public ImageResponse? LeftImage { get; set; }
  public ImageResponse? RightImage { get; set; }
  public ImageResponse? FrontImage { get; set; }
  public ImageResponse? BackImage { get; set; }
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
