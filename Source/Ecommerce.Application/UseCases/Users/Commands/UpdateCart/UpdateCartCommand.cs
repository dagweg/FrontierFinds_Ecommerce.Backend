using FluentResults;
using MediatR;

namespace Ecommerce.Application.UseCases.Users.Commands.UpdateCart;

public class UpdateCartCommand : IRequest<Result>
{
    public List<UpdateCartItemCommand> CartItems { get; set; } = [];
}
