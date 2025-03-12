using Ecommerce.Application.Common.Extensions;
using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.Common.Models;
using Ecommerce.Domain.ProductAggregate;
using Ecommerce.Domain.ProductAggregate.ValueObjects;
using Ecommerce.Domain.UserAggregate.ValueObjects;
using Ecommerce.Infrastructure.Common.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Infrastructure.Persistence.EfCore.Repositories;

public class ProductRepository : EfCoreRepository<Product, ProductId>, IProductRepository
{
  private readonly EfCoreContext _context;
  private readonly ILogger<ProductRepository> _logger;

  public ProductRepository(EfCoreContext context, ILogger<ProductRepository> logger)
    : base(context)
  {
    _context = context;
    _logger = logger;
  }

  public async Task<DeleteResult> BulkDeleteByIdAsync(IEnumerable<ProductId> productIds)
  {
    var products = await _context
      .Products.Where(p => productIds.Contains(p.Id))
      .IncludeEverything()
      .ToListAsync(); //Important to await and get the list.

    List<string> cleanupObjectIds = new List<string>();

    // to delegate the task of external resource deletion to the providers later
    foreach (var product in products)
    {
      cleanupObjectIds.AddRange(
        new[]
        {
          product.Images?.Thumbnail?.ObjectIdentifier,
          product.Images?.LeftImage?.ObjectIdentifier,
          product.Images?.RightImage?.ObjectIdentifier,
          product.Images?.TopImage?.ObjectIdentifier,
          product.Images?.BackImage?.ObjectIdentifier,
          product.Images?.BottomImage?.ObjectIdentifier,
          product.Images?.FrontImage?.ObjectIdentifier,
        }.Where(objectId => objectId != null)!
      );
    }

    var deletionResult = await _context
      .Products.Where(p => productIds.Contains(p.Id))
      .ExecuteDeleteAsync();

    return new DeleteResult
    {
      CleanupObjectIds = cleanupObjectIds,
      TotalItemsDeleted = deletionResult,
    };
  }

  public async Task<IDictionary<ProductId, Product>> BulkGetByIdAsync(
    IEnumerable<ProductId> productIds
  )
  {
    var products = await _context
      .Products.Where(p => productIds.Contains(p.Id))
      .IncludeEverything()
      .ToDictionaryAsync(kvp => kvp.Id, kvp => kvp);

    if (productIds.Count() != products.Count())
    {
      _logger.LogWarning("Some products were not found in the database");
    }

    return products;
  }

  public async Task<GetResult<Product>> GetAllProductsSellerNotListedAsync(
    UserId sellerId,
    PaginationParameters paginationParameters
  )
  {
    var items = await _context
      .Products.Where(p => p.SellerId != sellerId)
      .Paginate(paginationParameters)
      .IncludeEverything()
      .ToListAsync();

    return new GetResult<Product>
    {
      Items = items,
      TotalItems = items.Count(),
      TotalItemsFetched = items.Count(),
    };
  }

  public async Task<GetResult<Product>> GetBySellerAsync(
    UserId sellerId,
    PaginationParameters paginationParameters
  )
  {
    var totalProds = _context.Products.AsQueryable();
    var paginated = await totalProds
      .Where(p => p.SellerId == sellerId)
      .Paginate(paginationParameters)
      .IncludeEverything()
      .ToListAsync();

    return new GetResult<Product>
    {
      Items = paginated,
      TotalItems = totalProds.Count(),
      TotalItemsFetched = paginated.Count,
    };
  }

  public Task GetProductImageAsync(ProductId productId, string objectIdentifier)
  {
    throw new NotImplementedException();
  }
}
