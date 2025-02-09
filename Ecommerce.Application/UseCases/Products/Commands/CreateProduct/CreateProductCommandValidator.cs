using Ecommerce.Application.UseCases.Images.Commands;
using FluentValidation;

namespace Ecommerce.Application.UseCases.Products.CreateUser.Commands;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
  private readonly CreateImageCommandValidator _createImageCommandValidator;

  public CreateProductCommandValidator(CreateImageCommandValidator createImageCommandValidator)
  {
    _createImageCommandValidator = createImageCommandValidator;

    RuleFor(x => x.ProductName).NotEmpty();

    RuleFor(x => x.ProductDescription).NotEmpty();

    RuleFor(x => x.StockQuantity).NotEmpty().GreaterThan(0);

    RuleFor(x => x.PriceValue).NotEmpty().GreaterThan(0);

    RuleFor(x => x.PriceCurrency).NotEmpty().MaximumLength(3);

    string msg =
      $"Invalid image format. Format should comprise of {string.Join(", ", typeof(CreateImageCommand).GetProperties().Select(p => p.Name))}";
    RuleFor(x => x.Thumbnail)
      .Must(x => x != null && _createImageCommandValidator.Validate(x).IsValid)
      .WithMessage(msg);

    When(
      x => x.LeftImage is not null,
      () => RuleFor(x => x.LeftImage!).SetValidator(_createImageCommandValidator)
    );

    When(
      x => x.RightImage is not null,
      () => RuleFor(x => x.RightImage!).SetValidator(_createImageCommandValidator)
    );

    When(
      x => x.FrontImage is not null,
      () => RuleFor(x => x.FrontImage!).SetValidator(_createImageCommandValidator)
    );

    When(
      x => x.BackImage is not null,
      () => RuleFor(x => x.BackImage!).SetValidator(_createImageCommandValidator)
    );

    RuleFor(x => x.Tags).NotNull().ForEach(x => x.MaximumLength(50));
  }
}
