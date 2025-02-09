using Ecommerce.Application.Common.Extensions;
using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.Common.Models;
using Ecommerce.Domain.ProductAggregate;
using Ecommerce.Domain.ProductAggregate.ValueObjects;
using Ecommerce.Domain.UserAggregate.ValueObjects;
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

  public async Task<IDictionary<ProductId, Product>> BulkGetByIdAsync(
    IEnumerable<ProductId> productIds
  )
  {
    var products = await _context
      .Products.Where(p => productIds.Contains(p.Id))
      .ToDictionaryAsync(kvp => kvp.Id, kvp => kvp);

    if (productIds.Count() != products.Count())
    {
      _logger.LogWarning("Some products were not found in the database");
    }

    return products;
  }

  public async Task<GetAllResult<Product>> GetBySellerAsync(
    UserId sellerId,
    PaginationParameters paginationParameters
  )
  {
    var totalProds = _context.Products.AsQueryable();
    var paginated = await totalProds
      .Where(p => p.SellerId == sellerId)
      .Paginate(paginationParameters)
      .ToListAsync();

    return new GetAllResult<Product>
    {
      Items = paginated,
      TotalItems = totalProds.Count(),
      TotalItemsFetched = paginated.Count,
    };
  }
}
