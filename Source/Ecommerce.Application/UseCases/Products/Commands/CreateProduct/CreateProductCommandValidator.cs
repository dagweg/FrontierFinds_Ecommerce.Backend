using Ecommerce.Application.Common.Interfaces.Processors;
using Ecommerce.Application.UseCases.Images.CreateImage;
using FluentValidation;
using Newtonsoft.Json;

namespace Ecommerce.Application.UseCases.Products.CreateUser.Commands;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
  public CreateProductCommandValidator(IImageProcessor imageProcessor)
  {
    RuleFor(x => x.ProductName).NotEmpty();

    RuleFor(x => x.ProductDescription).NotEmpty();

    RuleFor(x => x.StockQuantity).NotEmpty().GreaterThan(0);

    RuleFor(x => x.PriceValueInCents).NotEmpty().GreaterThan(0);

    RuleFor(x => x.PriceCurrency).NotEmpty().MaximumLength(3);

    RuleFor(x => x.Thumbnail)
      .SetValidator(x => CreateImageCommandValidator.GetValidator(imageProcessor));

    RuleFor(x => x.LeftImage!)
      .SetValidator(x =>
        CreateImageCommandValidator.GetValidator(
          imageProcessor,
          isRequired: x.LeftImage is not null
        )
      );

    RuleFor(x => x.RightImage!)
      .SetValidator(x =>
        CreateImageCommandValidator.GetValidator(
          imageProcessor,
          isRequired: x.RightImage is not null
        )
      );

    RuleFor(x => x.FrontImage!)
      .SetValidator(x =>
        CreateImageCommandValidator.GetValidator(
          imageProcessor,
          isRequired: x.FrontImage is not null
        )
      );

    RuleFor(x => x.BackImage!)
      .SetValidator(x =>
        CreateImageCommandValidator.GetValidator(
          imageProcessor,
          isRequired: x.BackImage is not null
        )
      );

    RuleFor(x => x.Tags)
      .NotNull()
      .ForEach(x => x.MaximumLength(50))
      .Must(t => t.Count() <= 10)
      .WithMessage("Tags must not exceed 10");

    RuleFor(x => x.Categories).NotEmpty().WithMessage("Categories must not be empty");
  }
}
