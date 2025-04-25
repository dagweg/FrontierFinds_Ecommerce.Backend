namespace Ecommerce.Application.Common.Models.Search.Elastic;

// Define a simple enum for sorting direction
public enum SortDirection
{
  Asc,
  Desc,
}

public record ElasticSearchFilterParams
{
  // Required: The index or indices to search
  public required string Index { get; init; }

  // Optional: Full-text search term
  public string? SearchTerm { get; init; }

  // Optional: Filtering parameters (using nullable types for optional values)
  public decimal? MinPrice { get; init; }
  public decimal? MaxPrice { get; init; }

  public List<string>? Categories { get; init; } // List of category names/slugs to filter by
  public List<string>? Tags { get; init; } // List of tag names to filter by

  public decimal? MinRating { get; init; }
  public decimal? MaxRating { get; init; } // Less common, usually just minRating

  public string? SellerId { get; init; } // Filter by seller ID

  // Optional: Pagination
  public int Page { get; init; } = 1; // Default to first page
  public int PageSize { get; init; } = 10; // Default page size

  // Optional: Sorting
  public string? SortBy { get; init; } // Field name to sort by (e.g., "name", "price", "averageRating")
  public SortDirection SortDirection { get; init; } = SortDirection.Asc; // Default sort direction

  // Add any other parameters you might need for filtering, e.g.:
  // public bool? IsOnPromotion { get; init; }
  // public int? MinStock { get; init; }
  // public DateTime? CreatedAfter { get; init; }
  // public DateTime? CreatedBefore { get; init; }
}
