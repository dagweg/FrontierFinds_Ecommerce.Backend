namespace Ecommerce.Application.UseCases.Products.Common;

public class CartItemResult
{
    public required string Id { get; set; }
    public required int Quantity { get; set; }
    public required ProductResult Product { get; set; }
}
