using FluentResults;
using MediatR;

namespace Ecommerce.Application.UseCases.Users.Commands.RemoveFromCart;

public class RemoveFromCartCommand : IRequest<Result>
{
    public List<string> CartItemIds { get; init; } = [];
}
