using Ecommerce.Application.UseCases.Products.Common;
using FluentResults;
using MediatR;

namespace Ecommerce.Application.UseCases.Users.Commands.RemoveFromCart;

public class RemoveFromCartCommand : IRequest<Result<CartResult>>
{
  public string CartItemId { get; set; } = null!;
}
