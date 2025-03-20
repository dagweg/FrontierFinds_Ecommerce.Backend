using Ecommerce.Application.Common.Models;
using Ecommerce.Application.UseCases.Products.Common;
using Ecommerce.Application.UseCases.Products.Queries.GetFilteredProducts;
using Ecommerce.Domain.Common.Entities;
using Ecommerce.Domain.ProductAggregate;
using Ecommerce.Domain.ProductAggregate.Entities;
using Ecommerce.Domain.ProductAggregate.ValueObjects;
using Ecommerce.Domain.UserAggregate.ValueObjects;
using FluentResults;

namespace Ecommerce.Application.Common.Interfaces.Persistence;

public interface IProductReviewRepository
{
  Task<GetResult<ProductReviewResult>?> GetReviewsByProductSlug(
    string slug,
    PaginationParameters paginationParameters
  );
}
