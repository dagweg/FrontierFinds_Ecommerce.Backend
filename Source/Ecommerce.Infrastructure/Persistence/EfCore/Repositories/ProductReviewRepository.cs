using Ecommerce.Application.Common.Errors;
using Ecommerce.Application.Common.Extensions;
using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.Common.Models;
using Ecommerce.Application.UseCases.Products.Common;
using Ecommerce.Application.UseCases.Users.Common;
using Ecommerce.Domain.ProductAggregate;
using Ecommerce.Domain.ProductAggregate.Entities;
using Ecommerce.Domain.ProductAggregate.ValueObjects;
using Ecommerce.Domain.UserAggregate.ValueObjects;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Persistence.EfCore.Repositories;

public class ProductReviewRepository(
  EfCoreContext context,
  IUserRepository userRepository,
  IProductRepository productRepository
) : IProductReviewRepository
{
  private readonly EfCoreContext context = context;
  private readonly IUserRepository userRepository = userRepository;
  private readonly IProductRepository productRepository = productRepository;

  public async Task<GetResult<ProductReviewResult>?> GetReviewsByProductSlug(
    string slug,
    PaginationParameters paginationParameters
  )
  {
    var product = context.Products.Where(p => p.Slug == slug);

    if (product is null)
      return null;

    var reviews = product.Include(p => p.Reviews).SelectMany(p => p.Reviews).AsNoTracking();

    var reviewerIds = await reviews
      .Select(r => r.ReviewerId.Value)
      .Paginate(paginationParameters)
      .Distinct()
      .ToListAsync();

    var reviewers = await userRepository.BulkGetById(reviewerIds);

    var paginated = await reviews.Paginate(paginationParameters).ToListAsync();

    return new GetResult<ProductReviewResult>
    {
      TotalItems = reviews.Count(),
      TotalItemsFetched = paginated.Count(),
      Items = paginated.Select(r => new ProductReviewResult
      {
        Description = r.Description,
        Rating = r.Rating,
        Reviewer = new ProductReviewUserResult
        {
          Fullname =
            $"{reviewers[r.ReviewerId.Value].FirstName.Value} {reviewers[r.ReviewerId.Value].LastName.Value}",
          Id = r.ReviewerId,
          ProfileImageUrl = reviewers[r.ReviewerId.Value].ProfileImage?.Url,
        },
      }),
    };
  }
}
