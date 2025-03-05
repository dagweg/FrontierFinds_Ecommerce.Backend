using Ecommerce.Application.UseCases.Common.Interfaces;
using Ecommerce.Application.UseCases.Products.Common;
using FluentResults;
using MediatR;

namespace Ecommerce.Application.UseCases.Users.Commands.AddToCart;

public record AddToCartCommand : IRequest<Result>
{
  public List<CreateCartItemCommand> createCartItemCommands = [];
}
