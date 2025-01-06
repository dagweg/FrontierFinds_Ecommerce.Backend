using Ecommerce.Domain.Common.Models;

namespace Ecommerce.Domain.ProductAggregate.Entities;

public sealed class Promotion : Entity<Guid>
{
  public int DiscountPercentage { get; set; }
  public DateTime StartDate { get; set; }
  public DateTime EndDate { get; set; }

  private Promotion()
    : base(Guid.NewGuid()) { }

  private Promotion(Guid id, int discountPercentage, DateTime startDate, DateTime endDate)
    : base(id)
  {
    DiscountPercentage = discountPercentage;
    StartDate = startDate;
    EndDate = endDate;
  }

  public static Promotion Create(int discountPercentage, DateTime startDate, DateTime endDate)
  {
    if (discountPercentage < 0 || discountPercentage > 100)
    {
      throw new ArgumentException("Discount percentage must be between 0 and 100");
    }

    return new(Guid.NewGuid(), discountPercentage, startDate, endDate);
  }

  public override IEnumerable<object> GetEqualityComponents()
  {
    yield return Id;
  }
}
