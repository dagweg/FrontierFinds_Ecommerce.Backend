using System.Linq.Expressions;
using Ecommerce.Application.Common.Errors;
using Ecommerce.Application.Common.Extensions;
using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.Common.Models;
using Ecommerce.Application.Common.Utilities;
using Ecommerce.Application.UseCases.Products.Common;
using Ecommerce.Application.UseCases.Products.Queries.GetFilteredProducts;
using Ecommerce.Domain.Common.Entities;
using Ecommerce.Domain.OrderAggregate.ValueObjects;
using Ecommerce.Domain.ProductAggregate;
using Ecommerce.Domain.ProductAggregate.Entities;
using Ecommerce.Domain.ProductAggregate.ValueObjects;
using Ecommerce.Domain.UserAggregate.ValueObjects;
using Ecommerce.Infrastructure.Common.Extensions;
using Elastic.Clients.Elasticsearch;
using FluentResults;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Npgsql;

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

  public new async Task<GetProductsResult> GetAllAsync(PaginationParameters paginationParameters)
  {
    return await GetFilteredProductsAsync(
      new FilterProductsQuery { PaginationParameters = paginationParameters }
    );
  }

  public (long, long) GetMinMaxPrices(IQueryable<Product> product)
  {
    // (long minPrice, long maxPrice) minMax = product
    //   .Select(p => p.Price.ValueInCents)
    //   .AsEnumerable()
    //   .Aggregate(
    //     (long.MaxValue, 0), // Unnamed tuple as seed
    //     (acc, current) =>
    //       (
    //         Math.Min(acc.Item1, current), // Use Item1, Item2 for unnamed tuple
    //         Math.Max(acc.Item2, current)
    //       )
    //   );

    if (product == null || !product.Any())
    {
      return (0, 1000); // Return default values if the collection is empty
    }
    return (
      product.Select(p => p.Price.ValueInCents).Min(),
      product.Select(p => p.Price.ValueInCents).Max()
    );
  }

  public static Expression<Func<Product, bool>> GetFilterByCriteria(
    FilterProductsQuery filterProductsQuery
  )
  {
    return prod =>
      (
        filterProductsQuery.SubjectFilter == SubjectFilter.SellerProductsOnly // Seller Products only
          ? prod.SellerId == filterProductsQuery.SellerId
        : filterProductsQuery.SubjectFilter == SubjectFilter.AllProductsWithoutSeller // ALl products withoutseller
          ? prod.SellerId != filterProductsQuery.SellerId
        : true
      )
      && (
        filterProductsQuery.SearchTerm == null
        || prod.Name.Value.ToLower().Contains(filterProductsQuery.SearchTerm.ToLower().Trim())
      )
      && (
        filterProductsQuery.MinPriceValueInCents == null
        || prod.Price.ValueInCents >= filterProductsQuery.MinPriceValueInCents
      )
      && (
        filterProductsQuery.MaxPriceValueInCents == null
        || prod.Price.ValueInCents <= filterProductsQuery.MaxPriceValueInCents
      )
      && (
        filterProductsQuery.CategoryIds == null
        || !filterProductsQuery.CategoryIds.Any()
        || prod.Categories.Any(c => filterProductsQuery.CategoryIds.Contains(c.Id))
      );
  }

  public async Task<FluentResults.Result<DeleteResult>> BulkDeleteByIdAsync(
    IEnumerable<ProductId> productIds
  )
  {
    try
    {
      var products = await _context
        .Products.Where(p => productIds.Contains(p.Id))
        .IncludeEverything()
        .ToListAsync(); //Important to await and get the list.

      if (!products.Any())
        return new DeleteResult { CleanupObjectIds = [], TotalItemsDeleted = 0 };

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

      return FluentResults.Result.Ok(
        new DeleteResult { CleanupObjectIds = cleanupObjectIds, TotalItemsDeleted = deletionResult }
      );
    }
    catch (PostgresException pgEx)
      when (pgEx.SqlState == PostgresErrorCodes.ForeignKeyViolation
        && pgEx.ConstraintName == "FK_OrderItems_Products_ProductId"
      )
    {
      pgEx.ToString().Dump();
      return ForbiddenError.GetResult(
        "Products.Delete",
        "There are orders made for this product and for that reason you are not allowed to delete it at the moment."
      );
    }
    catch (Exception e)
    {
      e.Message.Dump();
      return InternalError.GetResult("An internal server error occured trying to delete a product");
    }
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

  public async Task<GetProductsResult> GetAllProductsSellerNotListedAsync(
    UserId sellerId,
    PaginationParameters paginationParameters,
    FilterProductsQuery? filterQuery = null
  )
  {
    var criteria = filterQuery != null ? GetFilterByCriteria(filterQuery) : p => true; // Default filter: always true
    var totalItems = _context.Products.Where(p => p.SellerId != sellerId).Where(criteria);

    var (minPrice, maxPrice) = GetMinMaxPrices(totalItems);

    var items = await totalItems
      .AsQueryable()
      .Paginate(paginationParameters)
      .IncludeEverything()
      .ToListAsync();

    return new GetProductsResult
    {
      MinPriceValueInCents = minPrice,
      MaxPriceValueInCents = maxPrice,
      Items = items,
      TotalItems = totalItems.Count(),
      TotalItemsFetched = items.Count(),
    };
  }

  public async Task<GetProductsResult> GetBySellerAsync(
    UserId sellerId,
    PaginationParameters paginationParameters,
    FilterProductsQuery? filterQuery = null
  )
  {
    var criteria = filterQuery != null ? GetFilterByCriteria(filterQuery) : p => true; // Default filter: always true
    var result = _context.Products.Where(p => p.SellerId == sellerId).Where(criteria);
    var paginated = await result.Paginate(paginationParameters).IncludeEverything().ToListAsync();

    var (minPrice, maxPrice) = GetMinMaxPrices(result);
    return new GetProductsResult
    {
      MinPriceValueInCents = minPrice,
      MaxPriceValueInCents = maxPrice,
      Items = paginated,
      TotalItems = result.Count(),
      TotalItemsFetched = paginated.Count(),
    };
  }

  public Task GetProductImageAsync(ProductId productId, string objectIdentifier)
  {
    throw new NotImplementedException();
  }

  public async Task<CategoriesResult> GetCategoriesAsync()
  {
    var categories = await _context.Categories.AsNoTracking().ToListAsync();

    var categoryDict = categories.ToDictionary(
      kvp => kvp.Id,
      kvp => new CategoryResult
      {
        Id = kvp.Id,
        Name = kvp.Name,
        Slug = kvp.Slug,
        IsActive = kvp.IsActive,
        ParentId = kvp.ParentId,
      }
    );

    foreach (var category in categories)
    {
      if (
        category.ParentId.HasValue
        && categoryDict.TryGetValue(category.ParentId.Value, out var parent)
      )
      {
        parent.SubCategories.Add(categoryDict[category.Id]);
      }
    }

    return new CategoriesResult
    {
      Categories = categoryDict
        .Values.Where(dto => !categories.Any(c => dto.Id == c.Id && c.ParentId.HasValue))
        .ToList(),
    };
  }

  public async Task<GetProductsResult> GetFilteredProductsAsync(
    FilterProductsQuery filterProductsQuery
  )
  {
    var criteria = GetFilterByCriteria(filterProductsQuery);
    var result = _context.Products.IncludeEverything().Where(criteria);

    var (minPrice, maxPrice) = (0L, 1000L);
    var paginated = new List<Product>();
    if (result != null)
    {
      (minPrice, maxPrice) = GetMinMaxPrices(result);
      paginated = await result.Paginate(filterProductsQuery.PaginationParameters).ToListAsync();
    }

    return new GetProductsResult
    {
      MinPriceValueInCents = minPrice,
      MaxPriceValueInCents = maxPrice,
      TotalItems = result == null ? 0 : result.Count(),
      TotalItemsFetched = paginated.Count(),
      Items = paginated,
    };
  }

  public async Task<IEnumerable<Category>> GetCategoriesById(IEnumerable<int> categoryIds)
  {
    var result = _context.Categories.Where(c => categoryIds.Contains(c.Id));

    result = result.Concat(
      result.SelectMany(c => c.SubCategories).Where(sc => categoryIds.Contains(sc.Id))
    );

    return await result.ToListAsync();
    ;
  }

  public async Task<IEnumerable<ProductTag>> GetOrCreateTags(IEnumerable<string> tags)
  {
    if (!tags.Any())
      return Enumerable.Empty<ProductTag>();

    // Normalize tags to avoid case-sensitivity issues
    var normalizedTags = tags.Select(t => t.Trim().ToLowerInvariant()).Distinct().ToList();

    // Fetch existing tags with full entities
    var existingTags = await _context
      .ProductTags.Where(p => normalizedTags.Contains(p.Name))
      .ToListAsync();

    // Create ProductTag instances for all input tags
    var productTags = normalizedTags.Select(t => ProductTag.Create(t.ToLower())).ToList();

    // Identify new tags by comparing names (case-insensitive)
    var newTags = productTags
      .Where(pt =>
        !existingTags.Any(et => et.Name.Equals(pt.Name, StringComparison.OrdinalIgnoreCase))
      )
      .ToList();

    if (newTags.Any())
    {
      _context.ProductTags.AddRange(newTags);
      Console.WriteLine($"{newTags.Count()} entities added.");
    }
    else
    {
      Console.WriteLine("No new entities to add.");
    }

    // Return all tags (existing + newly created)
    return existingTags.Concat(newTags).DistinctBy(t => t.Name, StringComparer.OrdinalIgnoreCase);
  }

  public async Task<Product?> GetProductBySlugAsync(string slug)
  {
    return await _context
      .Products.Where(p => p.Slug == slug)
      .IncludeEverything()
      .FirstOrDefaultAsync();
  }

  public Task UpdateAsync(Product product)
  {
    if (product == null)
    {
      _logger.LogWarning("Attempted to update a null Product entity.");
      throw new ArgumentNullException(nameof(product), "Product cannot be null.");
    }

    try
    {
      // Log the update attempt
      _logger.LogInformation("Updating Product with ID {ProductId}", product.Id);

      // Attach or update the entity in the context
      var entry = _context.Entry(product);
      if (entry.State == EntityState.Detached)
      {
        // If the entity is detached, attach it and mark it as modified
        _context.Products.Update(product);
      }
      else
      {
        // If already tracked, mark it as modified
        entry.State = EntityState.Modified;
      }

      _logger.LogInformation("Successfully updated Product with ID {ProductId}", product.Id);
    }
    catch (DbUpdateConcurrencyException ex)
    {
      _logger.LogError(
        ex,
        "Concurrency error while updating Product with ID {ProductId}",
        product.Id
      );
      throw; // Re-throw to let the caller handle concurrency
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Error updating Product with ID {ProductId}", product.Id);
      throw; // Re-throw for higher-level handling
    }

    return Task.CompletedTask;
  }

  public async Task<int> CountProducts()
  {
    return await _context.Products.CountAsync();
  }

  public IDictionary<OrderId, IEnumerable<Product>> GetMappedOrderIdWithProducts(
    IDictionary<OrderId, IEnumerable<ProductId>> dict
  )
  {
    return (IDictionary<OrderId, IEnumerable<Product>>)
      dict.Select(kvp =>
        {
          var orderId = kvp.Key;
          var productIds = kvp.Value;

          var mappedProducts = BulkGetByIdAsync(productIds).GetAwaiter().GetResult();

          return (orderId, mappedProducts.Values.AsEnumerable());
        })
        .ToDictionary(tuple => tuple.orderId, tuple => tuple.Item2);
  }
}
