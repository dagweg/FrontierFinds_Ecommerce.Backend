using Ecommerce.Application.UseCases.Common.Interfaces;
using Ecommerce.Application.UseCases.Products.Common;
using FluentResults;
using FluentValidation;
using MediatR;

namespace Ecommerce.Application.UseCases.Users.Commands.AddToCart;

public class AddToCartCommandValidator : AbstractValidator<AddToCartCommand>
{
  public AddToCartCommandValidator()
  {
    RuleForEach(x => x.createCartItemCommands)
      .Must(ci => Guid.TryParse(ci.ProductId, out _))
      .WithMessage("Invalid cart item product id")
      .Must(ci => ci.Quantity > 0)
      .WithMessage("Quantity must be greater than zero");
  }
}
