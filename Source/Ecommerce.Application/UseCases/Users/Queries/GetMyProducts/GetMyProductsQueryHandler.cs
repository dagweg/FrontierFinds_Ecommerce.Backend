using AutoMapper;
using Ecommerce.Application.Common.Extensions;
using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.Common.Interfaces.Providers.Context;
using Ecommerce.Application.Common.Models;
using Ecommerce.Application.UseCases.Products.Common;
using FluentResults;
using MediatR;

namespace Ecommerce.Application.UseCases.Users.Queries.GetMyProducts;

public class GetMyProductsQueryHandler : IRequestHandler<GetMyProductsQuery, Result<ProductsResult>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly IUserContextService _userContext;

    public GetMyProductsQueryHandler(
      IProductRepository productRepository,
      IMapper mapper,
      IUserContextService userContext
    )
    {
        _productRepository = productRepository;
        _mapper = mapper;
        _userContext = userContext;
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

        // get seller product listings
        var myProducts = await _productRepository.GetBySellerAsync(
          sellerId.Value,
          new PaginationParameters(request.PageNumber, request.PageSize)
        );

        var result = new ProductsResult
        {
            Products = myProducts.Items.Select(p => _mapper.Map<ProductResult>(p)),
            TotalCount = myProducts.TotalItems,
            TotalFetchedCount = myProducts.TotalItemsFetched,
        };

        return Result.Ok(result);
    }
}
