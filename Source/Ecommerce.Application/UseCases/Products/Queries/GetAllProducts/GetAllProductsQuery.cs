using Ecommerce.Application.UseCases.Common.Interfaces;
using Ecommerce.Application.UseCases.Products.Common;
using FluentResults;
using MediatR;

namespace Ecommerce.Application.UseCases.Products.Queries.GetAllProducts;

public class GetAllProductsQuery : IPaginated, IRequest<Result<ProductsResult>>
{
  public int PageNumber { get; init; }

  public int PageSize { get; init; }

  public GetAllProductsQuery(int pageNumber, int pageSize)
  {
    PageNumber = pageNumber;
    PageSize = pageSize;
  }
}
