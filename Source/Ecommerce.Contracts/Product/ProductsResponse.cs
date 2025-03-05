namespace Ecommerce.Contracts.Product;

public class ProductsResponse
{
    public required int TotalCount { get; set; }
    public required int FetchedCount { get; set; }
    public IEnumerable<ProductResponse> Products { get; set; } = [];
}
