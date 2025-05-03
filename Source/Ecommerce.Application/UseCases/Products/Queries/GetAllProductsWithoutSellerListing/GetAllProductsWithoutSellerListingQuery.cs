using Ecommerce.Application.UseCases.Common.Interfaces;
using Ecommerce.Application.UseCases.Products.Common;
using Ecommerce.Application.UseCases.Products.Queries.GetFilteredProducts;
using FluentResults;
using MediatR;

namespace Ecommerce.Application.UseCases.Products.Queries.GetAllProductsWithoutSellerListing;

public class GetAllProductsWithoutSellerListingQuery : IRequest<Result<ProductsResult>>
{
  public FilterProductsQuery? FilterQuery { get; set; }
}
