namespace Ecommerce.Application.UseCases.Products.Common;

public class PromotionResult
{
  public int DiscountPercentage { get; init; }
  public DateTime StartDate { get; init; }
  public DateTime EndDate { get; init; }
  public bool IsActive { get; init; }
}
