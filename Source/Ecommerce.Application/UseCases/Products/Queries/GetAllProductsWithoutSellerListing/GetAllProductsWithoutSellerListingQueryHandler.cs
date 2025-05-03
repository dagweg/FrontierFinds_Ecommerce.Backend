using AutoMapper;
using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.Common.Interfaces.Providers.Context;
using Ecommerce.Application.Common.Models;
using Ecommerce.Application.Common.Utilities;
using Ecommerce.Application.UseCases.Products.Common;
using Ecommerce.Application.UseCases.Products.Queries.GetFilteredProducts;
using Ecommerce.Domain.UserAggregate.ValueObjects;
using FluentResults;
using MediatR;

namespace Ecommerce.Application.UseCases.Products.Queries.GetAllProductsWithoutSellerListing;

public class GetAllProductsWithoutSellerListingQueryHandler
  : IRequestHandler<GetAllProductsWithoutSellerListingQuery, Result<ProductsResult>>
{
  private readonly IProductRepository _productRepository;
  private readonly IUserContextService _userContextService;
  private readonly ISender _sender;
  private readonly IMapper _mapper;

  public GetAllProductsWithoutSellerListingQueryHandler(
    IProductRepository productRepository,
    IMapper mapper,
    IUserContextService contextService,
    ISender sender
  )
  {
    _productRepository = productRepository;
    _userContextService = contextService;
    _mapper = mapper;
    _sender = sender;
  }

  public async Task<Result<ProductsResult>> Handle(
    GetAllProductsWithoutSellerListingQuery request,
    CancellationToken cancellationToken
  )
  {
    var userId = _userContextService.GetValidUserId();
    if (userId.IsFailed)
      return userId.ToResult();

    var result = await _sender.Send(
      new FilterProductsQuery()
      {
        SearchTerm = request.FilterQuery?.SearchTerm,
        CategoryIds = request.FilterQuery?.CategoryIds,
        MaxPriceValueInCents = request.FilterQuery?.MaxPriceValueInCents,
        MinPriceValueInCents = request.FilterQuery?.MinPriceValueInCents,
        SortBy = request.FilterQuery?.SortBy,
        SellerId = userId.Value,
        SubjectFilter = SubjectFilter.AllProductsWithoutSeller,
        PaginationParameters = request.FilterQuery is null
          ? new PaginationParameters()
          : request.FilterQuery.PaginationParameters,
      }
    );

    return result;
  }
}
