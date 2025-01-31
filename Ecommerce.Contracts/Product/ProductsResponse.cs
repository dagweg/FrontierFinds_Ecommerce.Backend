namespace Ecommerce.Contracts.Product;

public class ProductsResponse
{
  public int TotalCount { get; set; }
  public IEnumerable<ProductResponse> Products { get; set; } = [];
}
