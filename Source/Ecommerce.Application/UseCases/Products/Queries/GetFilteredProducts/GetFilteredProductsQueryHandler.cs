using AutoMapper;
using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.Common.Models;
using Ecommerce.Application.UseCases.Products.Common;
using FluentResults;
using MediatR;

namespace Ecommerce.Application.UseCases.Products.Queries.GetFilteredProducts;

public class GetFilteredProductsQueryHandler
  : IRequestHandler<FilterProductsQuery, Result<ProductsResult>>
{
  private readonly IProductRepository _productRepository;
  private readonly IMapper _mapper;

  public GetFilteredProductsQueryHandler(IProductRepository productRepository, IMapper mapper)
  {
    _productRepository = productRepository;
    _mapper = mapper;
  }

  public async Task<Result<ProductsResult>> Handle(
    FilterProductsQuery request,
    CancellationToken cancellationToken
  )
  {
    var products = await _productRepository.GetFilteredProductsAsync(
      request,
      request.PaginationParameters ?? new PaginationParameters()
    );

    return new ProductsResult
    {
      MinPriceValueInCents = products.MinPriceValueInCents,
      MaxPriceValueInCents = products.MaxPriceValueInCents,
      Products = products.Items.Select(p => _mapper.Map<ProductResult>(p)),
      TotalCount = products.TotalItems,
      TotalFetchedCount = products.TotalItemsFetched,
    };
  }
}
