using Ecommerce.Contracts.Image;
using Ecommerce.Contracts.Product.Common;

namespace Ecommerce.Contracts.Product;

public class ProductResponse
{
    public required string ProductId { get; set; }
    public required string ProductName { get; set; }
    public required string ProductDescription { get; set; }
    public required int StockQuantity { get; set; }
    public required decimal PriceValue { get; set; }
    public required string PriceCurrency { get; set; }
    public required ImageResponse Thumbnail { get; set; }
    public ProductImagesResponse? Images { get; set; }
    public List<TagResponse>? Tags { get; set; }
}
