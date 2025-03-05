namespace Ecommerce.Application.UseCases.Users.Commands.UpdateCart;

public class UpdateCartItemCommand
{
    public string CartItemId { get; set; } = null!;

    public int Quantity { get; set; }
}
