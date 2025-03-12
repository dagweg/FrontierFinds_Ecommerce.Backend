using Ecommerce.Application.UseCases.Images.CreateImage;
using FluentValidation;
using Newtonsoft.Json;

namespace Ecommerce.Application.UseCases.Products.CreateUser.Commands;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
  public CreateProductCommandValidator()
  {
    RuleFor(x => x.ProductName).NotEmpty();

    RuleFor(x => x.ProductDescription).NotEmpty();

    RuleFor(x => x.StockQuantity).NotEmpty().GreaterThan(0);

    RuleFor(x => x.PriceValueInCents).NotEmpty().GreaterThan(0);

    RuleFor(x => x.PriceCurrency).NotEmpty().MaximumLength(3);

    RuleFor(x => x.Thumbnail).SetValidator(x => CreateImageCommandValidator.GetValidator());

    RuleFor(x => x.LeftImage!)
      .SetValidator(x =>
        CreateImageCommandValidator.GetValidator(isRequired: x.LeftImage is not null)
      );

    RuleFor(x => x.RightImage!)
      .SetValidator(x =>
        CreateImageCommandValidator.GetValidator(isRequired: x.RightImage is not null)
      );

    RuleFor(x => x.FrontImage!)
      .SetValidator(x =>
        CreateImageCommandValidator.GetValidator(isRequired: x.FrontImage is not null)
      );

    RuleFor(x => x.BackImage!)
      .SetValidator(x =>
        CreateImageCommandValidator.GetValidator(isRequired: x.BackImage is not null)
      );

    RuleFor(x => x.Tags).NotNull().ForEach(x => x.MaximumLength(50));
  }
}
