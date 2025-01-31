using AutoMapper;
using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.UseCases.Products.Common;
using FluentResults;
using MediatR;

namespace Ecommerce.Application.UseCases.Products.Queries.GetAllProducts;

public class GetAllProductsQueryHandler
  : IRequestHandler<GetAllProductsQuery, Result<IEnumerable<ProductResult>>>
{
  private readonly IProductRepository _productRepository;
  private readonly IMapper _mapper;

  public GetAllProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
  {
    _productRepository = productRepository;
    _mapper = mapper;
  }

  public async Task<Result<IEnumerable<ProductResult>>> Handle(
    GetAllProductsQuery request,
    CancellationToken cancellationToken
  )
  {
    var products = await _productRepository.GetAllAsync(request.PageNumber, request.PageSize);

    var productResults = products.Select(p => _mapper.Map<ProductResult>(p));
    return Result.Ok(productResults);
  }
}
