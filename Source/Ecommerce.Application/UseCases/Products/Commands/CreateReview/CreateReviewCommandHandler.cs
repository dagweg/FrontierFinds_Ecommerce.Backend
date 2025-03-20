using AutoMapper;
using Ecommerce.Application.Common.Errors;
using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.Common.Interfaces.Providers.Context;
using Ecommerce.Application.UseCases.Products.Common;
using Ecommerce.Application.UseCases.Users.Common;
using Ecommerce.Domain.ProductAggregate.Entities;
using Ecommerce.Domain.ProductAggregate.ValueObjects;
using Ecommerce.Domain.UserAggregate.ValueObjects;
using FluentResults;
using MediatR;

namespace Ecommerce.Application.UseCases.Products.Commands.CreateReview;

public class CreateReviewCommmandHandler(
  IUserRepository userRepository,
  IProductRepository productRepository,
  IUnitOfWork unitOfWork,
  IUserContextService userContext,
  IMapper mapper
) : IRequestHandler<CreateReviewCommmand, Result<ProductReviewResult>>
{
  public async Task<Result<ProductReviewResult>> Handle(
    CreateReviewCommmand request,
    CancellationToken cancellationToken
  )
  {
    var reviewerIdRes = userContext.GetValidUserId();
    if (reviewerIdRes.IsFailed)
      return reviewerIdRes.ToResult();

    request.ReviewerId = reviewerIdRes.Value;

    var rating = Rating.Create(request.Rating);
    if (rating.IsFailed)
      return rating.ToResult();

    var reviewerId = UserId.Convert(request.ReviewerId);
    var productId = ProductId.Convert(request.ProductId);

    var user = await userRepository.GetByIdAsync(reviewerId);
    if (user is null)
      return NotFoundError.GetResult("user", "User not found");

    var review = ProductReview.Create(reviewerId, request.Description, rating.Value);

    var product = await productRepository.GetByIdAsync(productId);
    if (product is null)
      return NotFoundError.GetResult("product", "Product not found");

    product.AddRating(rating.Value);
    product.AddReview(review);

    var changeTrackerEntries = unitOfWork.GetChangeTrackerEntries();
    foreach (var entryInfo in changeTrackerEntries)
    {
      Console.WriteLine($"Entity: {entryInfo.EntityTypeName}, State: {entryInfo.State}");
    }

    await unitOfWork.SaveChangesAsync();

    return new ProductReviewResult
    {
      Description = review.Description,
      Rating = review.Rating.Value,
      Reviewer = mapper.Map<ProductReviewUserResult>(user),
    };
  }
}
