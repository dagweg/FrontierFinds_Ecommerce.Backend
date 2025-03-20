using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.Common.Utilities;
using Ecommerce.Domain.Common.Errors;
using Ecommerce.Domain.ProductAggregate.ValueObjects;
using FluentResults;

namespace Ecommerce.Application.Services.Utilities;

public interface ISlugService<TId>
{
  Task<Result<string>> GenerateUniqueSlugAsync(string name, TId? id = default);
}

public class ProductSlugService(IProductRepository productRepository) : ISlugService<ProductId>
{
  public async Task<Result<string>> GenerateUniqueSlugAsync(
    string productName,
    ProductId? productId = null
  )
  {
    if (string.IsNullOrWhiteSpace(productName))
      return FormatError.GetResult(nameof(productName), "Product name cannot be empty.");

    // Generate the base slug
    string baseSlug = SlugGenerator.GenerateSlug(productName);
    string slug = baseSlug;
    int suffix = 1;

    // Check if the slug exists in the database, excluding the current product (if updating)
    while (await productRepository.AnyAsync(p => p.Slug == slug))
    {
      // If it exists, append a suffix (e.g., "-1", "-2")
      slug = $"{baseSlug}-{suffix}";
      suffix++;
    }

    return slug;
  }
}
