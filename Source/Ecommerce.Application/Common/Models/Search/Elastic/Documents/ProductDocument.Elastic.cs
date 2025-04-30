using Ecommerce.Domain.ProductAggregate.Entities;

namespace Ecommerce.Application.Common.Models.Search.Elastic.Documents;

public class ProductDocument : ElasticDocumentBase
{
  public string Name { get; set; } = default!;
  public string Description { get; set; } = default!;
  public long PriceValueInCents { get; set; }
  public int StockQuantity { get; set; }
  public string Slug { get; set; } = default!;
  public Guid SellerId { get; set; }

  public List<CategoryDocument> Categories { get; set; } = new();

  public List<TagDocument> Tags { get; set; } = new();

  public int TotalReviews { get; set; }
  public decimal AverageRating { get; set; }

  public ProductImagesDocument ProductImages { get; set; } = default!;

  public PromotionDocument Promotion { get; set; } = default!;

  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
}
