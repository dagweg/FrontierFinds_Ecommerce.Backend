using AutoMapper;
using Ecommerce.Application.Common.Defaults;
using Ecommerce.Application.Common.Errors;
using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.Common.Interfaces.Providers.Search.Elastic;
using Ecommerce.Application.Common.Models;
using Ecommerce.Application.Common.Models.Search.Elastic;
using Ecommerce.Application.Common.Models.Search.Elastic.Documents;
using Ecommerce.Application.UseCases.Products.Common;
using Ecommerce.Domain.ProductAggregate.ValueObjects;
using FluentResults;
using MediatR;

namespace Ecommerce.Application.UseCases.Products.Queries.GetFilteredProducts;

public class GetFilteredProductsQueryHandler
  : IRequestHandler<FilterProductsQuery, Result<ProductsResult>>
{
  private readonly IProductRepository _productRepository;
  private readonly IElasticSearch<ProductDocument> _elasticSearch;
  private readonly IMapper _mapper;

  public GetFilteredProductsQueryHandler(
    IProductRepository productRepository,
    IMapper mapper,
    IElasticSearch<ProductDocument> elasticSearch
  )
  {
    _elasticSearch = elasticSearch;
    _productRepository = productRepository;
    _mapper = mapper;
  }

  public async Task<Result<ProductsResult>> Handle(
    FilterProductsQuery request,
    CancellationToken cancellationToken
  )
  {
    var productsFromElastic = await _elasticSearch.SearchAsync(
      new ElasticSearchFilterParams() { Index = ElasticIndices.ProductIndex }
    );

    GetProductsResult products;

    if (productsFromElastic is null || !productsFromElastic.Any()) // If nothing found in elastic search fallback to db search
    {
      Console.WriteLine("No products found in ElasticSearch, falling back to DB search.");
      products = await _productRepository.GetFilteredProductsAsync(
        request,
        request.PaginationParameters ?? new PaginationParameters()
      );
    }
    else // If found in elastic search, use that data
    {
      Console.WriteLine("Products found in ElasticSearch, using that data.");
      var productsFromDb = await _productRepository.BulkGetByIdAsync(
        productsFromElastic.Select(p => ProductId.Convert(Guid.Parse(p.Id))).ToList()
      );

      products = new GetProductsResult()
      {
        MinPriceValueInCents = productsFromElastic.Min(p => p.PriceValueInCents),
        MaxPriceValueInCents = productsFromElastic.Max(p => p.PriceValueInCents),
        Items = productsFromDb.Values,
        TotalItems = productsFromElastic.Count(),
        TotalItemsFetched = productsFromElastic.Count(),
      };
    }

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
