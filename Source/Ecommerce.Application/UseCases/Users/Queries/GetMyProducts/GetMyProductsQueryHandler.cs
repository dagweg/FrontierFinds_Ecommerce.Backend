using AutoMapper;
using Ecommerce.Application.Common.Extensions;
using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.Common.Interfaces.Providers.Context;
using Ecommerce.Application.Common.Models;
using Ecommerce.Application.Common.Utilities;
using Ecommerce.Application.UseCases.Products.Common;
using Ecommerce.Application.UseCases.Products.Queries.GetFilteredProducts;
using Ecommerce.Domain.UserAggregate.ValueObjects;
using FluentResults;
using MediatR;
using Microsoft.CodeAnalysis.CSharp;

namespace Ecommerce.Application.UseCases.Users.Queries.GetMyProducts;

public class GetMyProductsQueryHandler : IRequestHandler<GetMyProductsQuery, Result<ProductsResult>>
{
  private readonly IProductRepository _productRepository;
  private readonly IMapper _mapper;
  private readonly IUserContextService _userContext;
  private readonly ISender _sender;

  public GetMyProductsQueryHandler(
    IProductRepository productRepository,
    IMapper mapper,
    IUserContextService userContext,
    ISender sender
  )
  {
    _productRepository = productRepository;
    _mapper = mapper;
    _userContext = userContext;
    _sender = sender;
  }

  public async Task<Result<ProductsResult>> Handle(
    GetMyProductsQuery request,
    CancellationToken cancellationToken
  )
  {
    // get seller id
    var sellerId = _userContext.GetValidUserId();
    if (sellerId.IsFailed)
      return sellerId.ToResult();

    var result = await _sender.Send(
      new FilterProductsQuery()
      {
        SearchTerm = request.FilterQuery?.SearchTerm,
        CategoryIds = request.FilterQuery?.CategoryIds,
        MaxPriceValueInCents = request.FilterQuery?.MaxPriceValueInCents,
        MinPriceValueInCents = request.FilterQuery?.MinPriceValueInCents,
        SortBy = request.FilterQuery?.SortBy,
        SellerId = sellerId.Value,
        SubjectFilter = SubjectFilter.SellerProductsOnly,
        PaginationParameters = new PaginationParameters()
        {
          PageNumber = request.PageNumber,
          PageSize = request.PageSize,
        },
      }
    );

    if (result.IsFailed)
      return result.ToResult();

    return result.Value;
  }
}
