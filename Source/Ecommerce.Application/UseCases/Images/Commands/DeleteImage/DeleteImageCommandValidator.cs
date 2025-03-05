using Ecommerce.Application.UseCases.Images.Commands.DeleteImage;
using FluentValidation;

namespace Ecommerce.Application.UseCases.Images.Commands.DeleteImage;

public class DeleteImageCommandValidator : AbstractValidator<DeleteImageCommand>
{
  public DeleteImageCommandValidator()
  {
    RuleFor(x => x.ObjectIdentifier).NotEmpty();
  }
}
