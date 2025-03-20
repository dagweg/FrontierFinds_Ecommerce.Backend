using Ecommerce.Application.Common.Models;
using Ecommerce.Application.UseCases.Common.Interfaces;
using Ecommerce.Application.UseCases.Products.Common;
using Ecommerce.Application.UseCases.Products.Queries.GetFilteredProducts;
using FluentResults;
using MediatR;

namespace Ecommerce.Application.UseCases.Users.Queries.GetMyProducts;

public class GetMyProductsQuery : PaginationParameters, IRequest<Result<ProductsResult>>
{
  public FilterProductsQuery? FilterQuery { get; init; }

  public GetMyProductsQuery()
  {
    FilterQuery = new FilterProductsQuery(
      null,
      null,
      null,
      null,
      new PaginationParameters { PageNumber = PageNumber, PageSize = PageSize }
    );
  }
}
