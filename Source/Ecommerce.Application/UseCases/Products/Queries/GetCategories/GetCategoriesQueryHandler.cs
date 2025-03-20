using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.UseCases.Products.Common;
using FluentResults;
using MediatR;

namespace Ecommerce.Application.UseCases.Products.Queries.GetCategories;

public class GetCategoriesQueryHandler
  : IRequestHandler<GetCategoriesQuery, Result<CategoriesResult>>
{
  private readonly IProductRepository _productRepository;

  public GetCategoriesQueryHandler(IProductRepository productRepository)
  {
    _productRepository = productRepository;
  }

  public async Task<Result<CategoriesResult>> Handle(
    GetCategoriesQuery request,
    CancellationToken cancellationToken
  )
  {
    var categories = await _productRepository.GetCategoriesAsync();

    return categories;
  }
}
