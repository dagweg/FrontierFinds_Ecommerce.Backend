using AutoMapper;
using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.Common.Models;
using Ecommerce.Application.UseCases.Products.Common;
using FluentResults;
using MediatR;

namespace Ecommerce.Application.UseCases.Products.Queries.GetAllProducts;

public class GetAllProductsQueryHandler
  : IRequestHandler<GetAllProductsQuery, Result<ProductsResult>>
{
  private readonly IProductRepository _productRepository;
  private readonly IMapper _mapper;

  public GetAllProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
  {
    _productRepository = productRepository;
    _mapper = mapper;
  }

  public async Task<Result<ProductsResult>> Handle(
    GetAllProductsQuery request,
    CancellationToken cancellationToken
  )
  {
    var products = await _productRepository.GetAllAsync(
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
