namespace Ecommerce.Application.UseCases.Users.Commands.AddToCart;

public record CreateCartItemCommand
{
    public required string ProductId { get; init; }
    public int Quantity { get; init; } = 1;
}
