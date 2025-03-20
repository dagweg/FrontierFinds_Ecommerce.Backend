using Ecommerce.Application.UseCases.Users.Common;

namespace Ecommerce.Application.UseCases.Products.Common;

public class ProductReviewResult
{
  public required ProductReviewUserResult Reviewer { get; set; }
  public required string Description { get; set; }
  public required decimal Rating { get; set; }
}
