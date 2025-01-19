using FluentValidation;

namespace Ecommerce.Application.UseCases.Images.Commands;

public class CreateImageCommandValidator : AbstractValidator<CreateImageCommand>
{
  public CreateImageCommandValidator()
  {
    RuleFor(x => x.FileType).NotEmpty();

    When(
      x => x.FileSize.HasValue,
      () =>
      {
        RuleFor(x => x.FileSize).NotEmpty();
      }
    );
  }
}
