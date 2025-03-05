using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Ecommerce.Contracts.Image;

namespace Ecommerce.Contracts.Product;

public class CreateProductRequest
{
    [Required]
    [NotNull]
    public string ProductName { get; init; } = null!;

    [Required]
    [NotNull]
    public string ProductDescription { get; init; } = null!;

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Stock quantity must be greater than 1")]
    public int StockQuantity { get; init; }

    [Required]
    [Range(1, double.MaxValue, ErrorMessage = "Price value must be greater than 1")]
    public decimal PriceValue { get; init; }

    [Required]
    [NotNull]
    public string PriceCurrency { get; init; } = null!;

    public CreateImageRequest Thumbnail { get; init; } = null!;
    public CreateImageRequest? LeftImage { get; init; }
    public CreateImageRequest? RightImage { get; init; }
    public CreateImageRequest? FrontImage { get; init; }
    public CreateImageRequest? BackImage { get; init; }
    public List<string>? Tags { get; init; }
}
