using Ecommerce.Application.UseCases.Common.Interfaces;

namespace Ecommerce.Application.UseCases.Products.Common;

public class ProductReviewsResult : ICollectionQueryResult
{
  public required IEnumerable<ProductReviewResult> ProductReviews { get; set; }
  public int TotalCount { get; set; }
  public int TotalFetchedCount { get; set; }
}
