using Ecommerce.Application.Common.Models;
using Ecommerce.Application.UseCases.Products.Common;
using FluentResults;
using MediatR;

namespace Ecommerce.Application.UseCases.Products.Queries.GetReviewsByProductSlug;

public record GetReviewsByProductSlugQuery(string slug)
  : PaginationParametersImmutable,
    IRequest<Result<ProductReviewsResult>> { };
