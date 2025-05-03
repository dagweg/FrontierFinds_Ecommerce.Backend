using System.Runtime.CompilerServices;
using AutoMapper;
using Ecommerce.Application.Common.Defaults;
using Ecommerce.Application.Common.Errors;
using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.Common.Interfaces.Providers.Search.Elastic;
using Ecommerce.Application.Common.Models;
using Ecommerce.Application.Common.Models.Enums;
using Ecommerce.Application.Common.Models.Search.Elastic;
using Ecommerce.Application.Common.Models.Search.Elastic.Documents;
using Ecommerce.Application.Services.Workers.Elastic;
using Ecommerce.Application.UseCases.Products.Common;
using Ecommerce.Domain.ProductAggregate.ValueObjects;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Application.UseCases.Products.Queries.GetFilteredProducts;

public class GetFilteredProductsQueryHandler
  : IRequestHandler<FilterProductsQuery, Result<ProductsResult>>
{
  private readonly IProductRepository _productRepository;
  private readonly IElasticSearch _elasticSearch;
  private readonly IMapper _mapper;
  private readonly ILogger<GetFilteredProductsQueryHandler> _logger;
  private readonly IPublisher _publisher;

  public GetFilteredProductsQueryHandler(
    IProductRepository productRepository,
    IMapper mapper,
    IElasticSearch elasticSearch,
    ILogger<GetFilteredProductsQueryHandler> logger,
    IPublisher publisher
  )
  {
    _elasticSearch = elasticSearch;
    _productRepository = productRepository;
    _mapper = mapper;
    _logger = logger;
    _publisher = publisher;
  }

  public async Task<Result<ProductsResult>> Handle(
    FilterProductsQuery request,
    CancellationToken cancellationToken
  )
  {
    var productsFromElastic = await _elasticSearch.SearchAsync<ProductDocument>(
      new ElasticSearchFilterParams()
      {
        SubjectFilter = request.SubjectFilter,
        SellerId = request.SellerId,
        Index = ElasticIndices.ProductIndex,
        SearchTerm = request.SearchTerm,
        MaxPriceValueInCents = request.MaxPriceValueInCents,
        MinPriceValueInCents = request.MinPriceValueInCents,
        Pagination = request.PaginationParameters,
        SortBy = request.SortBy,
      }
    );

    if (productsFromElastic.Any()) // If elastic returns indexed items, return those
    {
      return new ProductsResult
      {
        DataSourceId = Guid.NewGuid().ToString(), // TODO: replace with acutal id of the data source
        DataSourceType = DataSourceType_Debug.Elastic,
        MinPriceValueInCents = productsFromElastic.Min(p => p.PriceValueInCents),
        MaxPriceValueInCents = productsFromElastic.Max(p => p.PriceValueInCents),
        Products = productsFromElastic.Select(p => _mapper.Map<ProductResult>(p)),
        TotalCount = await _productRepository.CountProducts(),
        TotalFetchedCount = productsFromElastic.Count(),
      };
    }

    _logger.LogInformation("No products found in ElasticSearch, falling back to DB search.");
    GetProductsResult products = await _productRepository.GetFilteredProductsAsync(request);

    // Schedule an indexing job for the non-indexed products for future searches
    await _publisher.Publish(
      new ElasticTaskNotification()
      {
        ElasticAction = ElasticAction.Index,
        IndexDocs = new Dictionary<string, ElasticDocumentBase[]>
        {
          {
            ElasticIndices.ProductIndex,
            products
              .Items.Select(v => (ElasticDocumentBase)_mapper.Map<ProductDocument>(v))
              .ToArray()
          },
        },
      }
    );

    return new ProductsResult
    {
      DataSourceType = DataSourceType_Debug.Db,
      MinPriceValueInCents = products.MinPriceValueInCents,
      MaxPriceValueInCents = products.MaxPriceValueInCents,
      Products = products.Items.Select(p => _mapper.Map<ProductResult>(p)),
      TotalCount = products.TotalItems,
      TotalFetchedCount = products.TotalItemsFetched,
    };
  }
}
