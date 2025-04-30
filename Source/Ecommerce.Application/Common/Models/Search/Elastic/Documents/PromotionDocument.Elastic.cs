namespace Ecommerce.Application.Common.Models.Search.Elastic.Documents;

public class PromotionDocument : ElasticDocumentBase
{
  public int DiscountPercentage { get; init; }
  public DateTime StartDate { get; init; }
  public DateTime EndDate { get; init; }
  public bool IsActive { get; init; }
}
