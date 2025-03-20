using Ecommerce.Application.UseCases.Products.Common;
using FluentResults;
using MediatR;

namespace Ecommerce.Application.UseCases.Products.Commands.CreateReview;

public class CreateReviewCommmand : IRequest<Result<ProductReviewResult>>
{
  public Guid ReviewerId { get; set; }
  public string Description { get; set; } = null!;
  public decimal Rating { get; set; }
  public Guid ProductId { get; set; }
}
