using AutoMapper;
using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.Common.Interfaces.Providers.Context;
using Ecommerce.Application.Common.Models;
using Ecommerce.Application.UseCases.Products.Common;
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
    var userIdR = _userContextService.GetValidUserId();
    if (userIdR.IsFailed)
      return userIdR.ToResult();

    var products = await _productRepository.GetAllProductsSellerNotListedAsync(
      userIdR.Value,
      new PaginationParameters(request.PageNumber, request.PageSize)
    );

    var result = new ProductsResult
    {
      Products = products.Items.Select(p => _mapper.Map<ProductResult>(p)),
      TotalCount = products.TotalItems,
      TotalFetchedCount = products.TotalItemsFetched,
    };

    return Result.Ok(result);
  }
}
