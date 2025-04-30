using Ecommerce.Application.Common.Models;
using Ecommerce.Application.Common.Models.Enums;
using Ecommerce.Application.UseCases.Products.Common;
using FluentResults;
using MediatR;

namespace Ecommerce.Application.UseCases.Products.Queries.GetFilteredProducts;

public record FilterProductsQuery : IRequest<Result<ProductsResult>>
{
  public SortBy SortBy { get; init; } = SortBy.None;
  public string? SearchTerm { get; init; }
  public long? MinPriceValueInCents { get; init; }
  public long? MaxPriceValueInCents { get; init; }
  public IEnumerable<int>? CategoryIds { get; init; }
  public PaginationParameters PaginationParameters { get; init; } = new();
}
