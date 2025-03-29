using Ecommerce.Application.UseCases.Products.Common;
using FluentResults;
using MediatR;

namespace Ecommerce.Application.UseCases.Users.Commands.UpdateCart;

public class UpdateCartCommand : IRequest<Result<CartResult>>
{
  // public List<UpdateCartItemCommand> CartItems { get; set; } = [];
  public required UpdateCartItemCommand CartItem;
}
