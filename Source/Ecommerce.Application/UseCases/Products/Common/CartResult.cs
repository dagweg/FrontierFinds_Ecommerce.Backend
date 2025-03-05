namespace Ecommerce.Application.UseCases.Products.Common;

public class CartResult
{
    public required int TotalItems { get; set; }
    public required decimal TotalPrice { get; set; }
    public IEnumerable<CartItemResult> Items { get; set; } = [];
}
