using AutoMapper;
using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.Common.Interfaces.Providers.Context;
using Ecommerce.Application.Common.Models;
using Ecommerce.Application.Common.Utilities;
using Ecommerce.Application.UseCases.Products.Common;
using Ecommerce.Domain.UserAggregate.ValueObjects;
using FluentResults;
using MediatR;

namespace Ecommerce.Application.UseCases.Products.Queries.GetAllProducts;

public class GetAllProductsQueryHandler
  : IRequestHandler<GetAllProductsQuery, Result<ProductsResult>>
{
  private readonly IProductRepository _productRepository;
  private readonly IUserContextService _userContextService;

  private readonly IMapper _mapper;

  public GetAllProductsQueryHandler(
    IProductRepository productRepository,
    IMapper mapper,
    IUserContextService contextService
  )
  {
    _productRepository = productRepository;
    _userContextService = contextService;
    _mapper = mapper;
  }

  public async Task<Result<ProductsResult>> Handle(
    GetAllProductsQuery request,
    CancellationToken cancellationToken
  )
  {
    var userId = _userContextService.GetValidUserId();

    GetProductsResult products;
    var pagination = new PaginationParameters
    {
      PageNumber = request.PageNumber,
      PageSize = request.PageSize,
    };

    if (userId.IsFailed)
    {
      products = await _productRepository.GetAllAsync(pagination);
    }
    else
    {
      products = await _productRepository.GetAllProductsSellerNotListedAsync(
        userId.Value,
        pagination
      );
    }

    var result = new ProductsResult
    {
      MaxPriceValueInCents = products.MaxPriceValueInCents,
      MinPriceValueInCents = products.MinPriceValueInCents,
      Products = products.Items.Select(p => _mapper.Map<ProductResult>(p)),
      TotalCount = products.TotalItems,
      TotalFetchedCount = products.TotalItemsFetched,
    };

    return Result.Ok(result);
  }
}
