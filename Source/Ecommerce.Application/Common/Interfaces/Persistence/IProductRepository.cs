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
  /// Deletes a product by its id
  /// </summary>
  /// <param name="productIds"></param>
  /// <returns>  </returns>
  Task<DeleteResult> BulkDeleteByIdAsync(IEnumerable<ProductId> productIds);

  /// <summary>
  /// Gets all the products the seller has listed.
  /// </summary>
  /// <param name="sellerId"></param>
  /// <param name="paginationParameters"></param>
  /// <returns>Enumeration of the products</returns>
  Task<GetResult<Product>> GetBySellerAsync(
    UserId sellerId,
    PaginationParameters paginationParameters
  );

  Task GetProductImageAsync(ProductId productId, string objectIdentifier);

  /// <summary>
  /// Gets all the products that are not listed by the seller
  /// </summary>
  /// <param name="paginationParameters"></param>
  /// <returns></returns>
  Task<GetResult<Product>> GetAllProductsSellerNotListedAsync(
    UserId sellerId,
    PaginationParameters paginationParameters
  );
}
