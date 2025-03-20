using System.Linq.Expressions;
using Ecommerce.Application.Common.Extensions;
using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.Common.Models;
using Ecommerce.Application.Common.Utilities;
using Ecommerce.Application.UseCases.Products.Common;
using Ecommerce.Application.UseCases.Products.Queries.GetFilteredProducts;
using Ecommerce.Domain.Common.Entities;
using Ecommerce.Domain.ProductAggregate;
using Ecommerce.Domain.ProductAggregate.Entities;
using Ecommerce.Domain.ProductAggregate.ValueObjects;
using Ecommerce.Domain.UserAggregate.ValueObjects;
using Ecommerce.Infrastructure.Common.Extensions;
using Microsoft.Data.SqlClient;
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

  public static Expression<Func<Product, bool>> GetFilterByCriteria(
    FilterProductsQuery filterProductsQuery
  )
  {
    return prod =>
      (
        filterProductsQuery.Name == null
        || prod.Name.Value.ToLower().Contains(filterProductsQuery.Name.ToLower().Trim())
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
    PaginationParameters paginationParameters,
    FilterProductsQuery? filterQuery = null
  )
  {
    var criteria = filterQuery != null ? GetFilterByCriteria(filterQuery) : p => true; // Default filter: always true
    var totalItems = _context.Products.Where(p => p.SellerId != sellerId).Where(criteria);

    var items = await totalItems
      .AsQueryable()
      .Paginate(paginationParameters)
      .IncludeEverything()
      .ToListAsync();

    return new GetResult<Product>
    {
      Items = items,
      TotalItems = totalItems.Count(),
      TotalItemsFetched = items.Count(),
    };
  }

  public async Task<GetResult<Product>> GetBySellerAsync(
    UserId sellerId,
    PaginationParameters paginationParameters,
    FilterProductsQuery? filterQuery = null
  )
  {
    var criteria = filterQuery != null ? GetFilterByCriteria(filterQuery) : p => true; // Default filter: always true
    var result = _context.Products.Where(p => p.SellerId == sellerId).Where(criteria);
    var paginated = await result.Paginate(paginationParameters).IncludeEverything().ToListAsync();

    return new GetResult<Product>
    {
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

  public async Task<GetResult<Product>> GetFilteredProductsAsync(
    FilterProductsQuery filterProductsQuery,
    PaginationParameters paginationParameters
  )
  {
    var criteria = GetFilterByCriteria(filterProductsQuery);
    var result = _context.Products.IncludeEverything().Where(criteria);

    var paginated = await result.Paginate(paginationParameters).ToListAsync();

    return new GetResult<Product>
    {
      TotalItems = result.Count(),
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
}
