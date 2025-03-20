using AutoMapper;
using Ecommerce.Application.Common.Errors;
using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.UseCases.Products.Common;
using FluentResults;
using MediatR;

namespace Ecommerce.Application.UseCases.Products.Queries.GetProductBySlug;

public class GetProductBySlugQueryHandler(IProductRepository productRepository, IMapper mapper)
  : IRequestHandler<GetProductBySlugQuery, Result<ProductResult>>
{
  public async Task<Result<ProductResult>> Handle(
    GetProductBySlugQuery request,
    CancellationToken cancellationToken
  )
  {
    var product = await productRepository.GetProductBySlugAsync(request.Slug);

    if (product is null)
    {
      return NotFoundError.GetResult(nameof(request.Slug), "Product not found");
    }

    return mapper.Map<ProductResult>(product);
  }
}
