using Ecommerce.Application.Common.Models;
using Ecommerce.Domain.ProductAggregate;
using Ecommerce.Domain.ProductAggregate.ValueObjects;
using Ecommerce.Domain.UserAggregate.ValueObjects;

namespace Ecommerce.Application.Common.Interfaces.Persistence;

public interface IProductRepository : IRepository<Product, ProductId>
{
  /// <summary>
  /// Get bulk of products by their ids
  /// </summary>
  /// <param name="productIds"></param>
  /// <returns>
  ///   A dictionary of products with their ids mapped to the product for fast lookup
  /// </returns>
  Task<IDictionary<ProductId, Product>> BulkGetByIdAsync(IEnumerable<ProductId> productIds);

  /// <summary>
  /// Gets all the products the seller has listed.
  /// </summary>
  /// <param name="sellerId"></param>
  /// <param name="paginationParameters"></param>
  /// <returns>Enumeration of the products</returns>
  Task<GetAllResult<Product>> GetBySellerAsync(
    UserId sellerId,
    PaginationParameters paginationParameters
  );

  Task GetProductImageAsync(ProductId productId, string objectIdentifier);
}
