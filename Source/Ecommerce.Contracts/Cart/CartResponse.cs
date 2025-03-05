namespace Ecommerce.Contracts.Cart;

public class CartResponse
{
    public int TotalCount { get; set; }
    public decimal TotalPrice { get; set; }
    public List<CartItemResponse> Items { get; set; }

    public CartResponse(List<CartItemResponse> items, decimal totalPrice)
    {
        Items = items;
        TotalPrice = totalPrice;
    }
};
