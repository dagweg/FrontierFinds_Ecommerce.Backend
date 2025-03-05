using Ecommerce.Domain.ProductAggregate.Entities;
using FluentValidation;

namespace Ecommerce.Application.UseCases.Images.CreateImage;

public class CreateImageCommandValidator : AbstractValidator<CreateImageCommand>
{
    public CreateImageCommandValidator(bool isRequired = true)
    {
        When(
          x => isRequired,
          () =>
            RuleFor(i => i.ImageStream)
              .Must(i => i.Length <= ProductImage.SIZE_LIMIT_BYTES)
              .WithMessage(
                $"Image size must be less than {ProductImage.SIZE_LIMIT_BYTES / (1024 * 1024)} MB."
              )
        );
    }

    public static CreateImageCommandValidator GetValidator(bool isRequired = true) =>
      new CreateImageCommandValidator(isRequired);
}
