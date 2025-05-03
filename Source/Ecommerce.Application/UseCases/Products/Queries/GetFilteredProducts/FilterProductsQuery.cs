using System.Linq.Expressions;
using Ecommerce.Application.Common.Models;
using Ecommerce.Application.Common.Models.Enums;
using Ecommerce.Application.UseCases.Products.Common;
using Ecommerce.Domain.ProductAggregate;
using Ecommerce.Domain.UserAggregate.ValueObjects;
using FluentResults;
using MediatR;

namespace Ecommerce.Application.UseCases.Products.Queries.GetFilteredProducts;

public enum SubjectFilter
{
  AllProducts,
  AllProductsWithoutSeller,
  SellerProductsOnly,
}

public record FilterProductsQuery : IRequest<Result<ProductsResult>>
{
  public UserId? SellerId { get; init; }
  public SubjectFilter SubjectFilter { get; init; } = SubjectFilter.AllProducts; // Fetch all products by default
  public SortBy? SortBy { get; init; }
  public string? SearchTerm { get; init; }
  public long? MinPriceValueInCents { get; init; }
  public long? MaxPriceValueInCents { get; init; }
  public IEnumerable<int>? CategoryIds { get; init; }
  public PaginationParameters PaginationParameters { get; init; } = new();
}
