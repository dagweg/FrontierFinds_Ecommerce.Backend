using Ecommerce.Application.Common.Models;
using Ecommerce.Application.UseCases.Products.Common;
using FluentResults;
using MediatR;

namespace Ecommerce.Application.UseCases.Products.Queries.GetFilteredProducts;

public record FilterProductsQuery(
  string? Name = null,
  long? MinPriceValueInCents = null,
  long? MaxPriceValueInCents = null,
  IEnumerable<int>? CategoryIds = null,
  PaginationParameters? PaginationParameters = null
) : IRequest<Result<ProductsResult>>;
