using Ecommerce.Application.UseCases.Images.Common;

namespace Ecommerce.Application.UseCases.Products.Common;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
public class ProductImagesResult
{
    public ProductImageResult Thumbnail { get; set; }
    public ProductImageResult? LeftImage { get; set; }
    public ProductImageResult? RightImage { get; set; }
    public ProductImageResult? FrontImage { get; set; }
    public ProductImageResult? BackImage { get; set; }
    public ProductImageResult? TopImage { get; set; }
    public ProductImageResult? BottomImage { get; set; }
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
