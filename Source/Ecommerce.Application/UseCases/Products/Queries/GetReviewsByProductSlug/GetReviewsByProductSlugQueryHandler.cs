using AutoMapper;
using Ecommerce.Application.Common.Errors;
using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.Common.Models;
using Ecommerce.Application.UseCases.Products.Common;
using FluentResults;
using MediatR;

namespace Ecommerce.Application.UseCases.Products.Queries.GetReviewsByProductSlug;

public class GetReviewsByProductSlugQueryHandler(IProductReviewRepository productReviewRepository)
  : IRequestHandler<GetReviewsByProductSlugQuery, Result<ProductReviewsResult>>
{
  public async Task<Result<ProductReviewsResult>> Handle(
    GetReviewsByProductSlugQuery request,
    CancellationToken cancellationToken
  )
  {
    var result = await productReviewRepository.GetReviewsByProductSlug(
      request.slug,
      new PaginationParameters { PageNumber = request.PageNumber, PageSize = request.PageSize }
    );

    if (result is null)
    {
      return NotFoundError.GetResult("product", "Product not found. Couldn't get reviews");
    }

    return new ProductReviewsResult
    {
      ProductReviews = result.Items,
      TotalCount = result.TotalItems,
      TotalFetchedCount = result.TotalItemsFetched,
    };
  }
}
