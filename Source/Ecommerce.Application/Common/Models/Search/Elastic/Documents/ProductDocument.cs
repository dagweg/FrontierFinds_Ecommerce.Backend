namespace Ecommerce.Application.Common.Models.Search.Elastic.Documents;

public class ProductDocument
{
  public required string Id { get; set; }
  public required string Name { get; set; }
  public required string Description { get; set; }
  public long PriceValueInCents { get; set; }
  public int Stock { get; set; }
  public required string Slug { get; set; }
  public required string SellerId { get; set; } // Also string
  public List<string> Categories { get; set; } = []; // Index category names or slugs
  public List<string> Tags { get; set; } = []; // Index tag names
  public int TotalReviews { get; set; } // Derived data is good for indexing
  public decimal AverageRating { get; set; } // Derived data is good for indexing
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
}
