using Ecommerce.Application.Common.Models;
using Ecommerce.Application.UseCases.Products.Common;
using Ecommerce.Application.UseCases.Products.Queries.GetFilteredProducts;
using Ecommerce.Domain.Common.Entities;
using Ecommerce.Domain.ProductAggregate;
using Ecommerce.Domain.ProductAggregate.Entities;
using Ecommerce.Domain.ProductAggregate.ValueObjects;
using Ecommerce.Domain.UserAggregate.ValueObjects;
using FluentResults;

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
  Task<Result<DeleteResult>> BulkDeleteByIdAsync(IEnumerable<ProductId> productIds);

  /// <summary>
  /// Gets all the products the seller has listed.
  /// </summary>
  /// <param name="sellerId"></param>
  /// <param name="paginationParameters"></param>
  /// <returns>Enumeration of the products</returns>
  Task<GetProductsResult> GetBySellerAsync(
    UserId sellerId,
    PaginationParameters paginationParameters,
    FilterProductsQuery? filterQuery = null
  );

  /// <summary>
  /// Get images of a given product
  /// </summary>
  /// <param name="productId"></param>
  /// <param name="objectIdentifier"></param>
  /// <returns></returns>
  Task GetProductImageAsync(ProductId productId, string objectIdentifier);

  /// <summary>
  /// Gets all the products that are not listed by the seller
  /// </summary>
  /// <param name="paginationParameters"></param>
  /// <returns></returns>
  Task<GetProductsResult> GetAllProductsSellerNotListedAsync(
    UserId sellerId,
    PaginationParameters paginationParameters,
    FilterProductsQuery? filterQuery = null
  );

  public new Task<GetProductsResult> GetAllAsync(PaginationParameters paginationParameters);

  /// <summary>
  /// Gets all supported categories
  /// </summary>
  /// <returns></returns>
  Task<CategoriesResult> GetCategoriesAsync();

  /// <summary>
  /// Gets categories by their ids
  /// </summary>
  /// <param name="categoryIds"></param>
  /// <returns></returns>
  Task<IEnumerable<Category>> GetCategoriesById(IEnumerable<int> categoryIds);

  /// <summary>
  /// Gets or creates tags
  /// </summary>
  /// <param name="tags"></param>
  /// <returns></returns>
  Task<IEnumerable<ProductTag>> GetOrCreateTags(IEnumerable<string> tags);

  /// <summary>
  /// Gets all products that pass filter parameter criteria
  /// </summary>
  /// <param name="filterProductsQuery"></param>
  /// <returns></returns>
  Task<GetProductsResult> GetFilteredProductsAsync(FilterProductsQuery filterProductsQuery);

  /// <summary>
  /// Gets a product by its slug
  /// </summary>
  /// <param name="slug"></param>
  /// <returns></returns>
  Task<Product?> GetProductBySlugAsync(string slug);

  /// <summary>
  /// Returns the total number of products in the db
  /// </summary>
  /// <returns></returns>
  Task<int> CountProducts();
}
