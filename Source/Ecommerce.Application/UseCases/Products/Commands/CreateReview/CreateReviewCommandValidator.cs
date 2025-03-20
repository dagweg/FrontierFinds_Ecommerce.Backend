using Ecommerce.Application.UseCases.Products.Commands.CreateReview;
using FluentValidation;

namespace Ecommerce.Application.UseCases.Products.Commands.CreateReview;

public class CreateReviewCommandValidator : AbstractValidator<CreateReviewCommmand>
{
  public CreateReviewCommandValidator()
  {
    RuleFor(x => x.Description)
      .NotEmpty()
      .WithMessage("Description is required.")
      .MinimumLength(5)
      .WithMessage("Description must be at least 5 characters long.")
      .MaximumLength(500)
      .WithMessage("Description cannot exceed 500 characters.");

    RuleFor(x => x.Rating)
      .InclusiveBetween(1, 5)
      .WithMessage("Rating must be between 1 and 5.")
      .PrecisionScale(2, 1, false)
      .WithMessage("Rating must have at most one decimal place (e.g., 4.5).");

    RuleFor(x => x.ProductId)
      .NotEmpty()
      .WithMessage("Product ID is required.")
      .Must(id => id != Guid.Empty)
      .WithMessage("Product ID must be a valid GUID.");
  }
}
