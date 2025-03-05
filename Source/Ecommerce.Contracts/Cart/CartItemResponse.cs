namespace Ecommerce.Contracts.Cart;

public class CartItemResponse
{
    public required string Id { get; set; }
    public required string ProductId { get; set; }
    public required string ProductName { get; set; }
    public required string ProductDescription { get; set; }
    public required string ProductImage { get; set; }
    public decimal ProductPrice { get; set; }
    public int Quantity { get; set; }
}
