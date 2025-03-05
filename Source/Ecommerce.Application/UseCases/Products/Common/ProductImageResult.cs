using Ecommerce.Application.UseCases.Images.Common;

namespace Ecommerce.Application.UseCases.Products.Common;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
public class ProductImageResult
{
    public required string? Url { get; set; }
}
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
