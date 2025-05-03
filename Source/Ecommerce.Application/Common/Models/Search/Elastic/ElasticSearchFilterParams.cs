using Ecommerce.Application.Common.Models.Enums;
using Ecommerce.Application.UseCases.Products.Queries.GetFilteredProducts;
using Ecommerce.Domain.UserAggregate.ValueObjects;

namespace Ecommerce.Application.Common.Models.Search.Elastic;

public record ElasticSearchFilterParams
{
  // Required: The index or indices to search
  public required string Index { get; init; }

  public UserId? SellerId { get; init; }
  public SubjectFilter SubjectFilter { get; init; } = SubjectFilter.AllProducts; // Fetch all products by default

  // Optional: Full-text search term
  public string? SearchTerm { get; init; }

  public long? MinPriceValueInCents { get; init; }
  public long? MaxPriceValueInCents { get; init; }

  public List<string>? Categories { get; init; } // List of category names/slugs to filter by
  public List<string>? Tags { get; init; } // List of tag names to filter by

  public decimal? MinRating { get; init; }
  public decimal? MaxRating { get; init; } // Less common, usually just minRating

  public PaginationParameters Pagination { get; init; } = default!;

  // Optional: Sorting
  public SortBy? SortBy { get; init; } // Field name to sort by (e.g., "name", "price", "averageRating")
  // Add any other parameters you might need for filtering, e.g.:
  // public bool? IsOnPromotion { get; init; }
  // public int? MinStock { get; init; }
  // public DateTime? CreatedAfter { get; init; }
  // public DateTime? CreatedBefore { get; init; }
}
