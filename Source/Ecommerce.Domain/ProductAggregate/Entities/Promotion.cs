using Ecommerce.Domain.Common.Errors;
using Ecommerce.Domain.Common.Models;
using FluentResults;

namespace Ecommerce.Domain.ProductAggregate.Entities;

public sealed class Promotion : Entity<Guid>
{
  public int DiscountPercentage { get; init; }
  public DateTime StartDate { get; init; }
  public DateTime EndDate { get; init; }
  public bool IsActive => StartDate <= DateTime.UtcNow && EndDate >= DateTime.UtcNow;
  private const int MIN_DISCOUNT_PERCENTAGE = 0;
  private const int MAX_DISCOUNT_PERCENTAGE = 100;

  private Promotion()
    : base(Guid.NewGuid()) { }

  private Promotion(Guid id, int discountPercentage, DateTime startDate, DateTime endDate)
    : base(id)
  {
    DiscountPercentage = discountPercentage;
    StartDate = startDate;
    EndDate = endDate;
  }

  public static Result<Promotion> Create(
    int discountPercentage,
    DateTime startDate,
    DateTime endDate
  )
  {
    if (
      discountPercentage < MIN_DISCOUNT_PERCENTAGE
      || discountPercentage > MAX_DISCOUNT_PERCENTAGE
    )
    {
      return OutOfBoundsError<int>.GetResult(
        nameof(DiscountPercentage),
        "Percentage is out of bounds.",
        MIN_DISCOUNT_PERCENTAGE,
        MAX_DISCOUNT_PERCENTAGE
      );
    }

    if (startDate >= endDate)
    {
      return InvalidDateRangeError.GetResult(
        nameof(startDate),
        "Range Invalid",
        startDate,
        endDate
      );
    }

    var promotion = new Promotion(Guid.NewGuid(), discountPercentage, startDate, endDate);

    return Result.Ok(promotion);
  }

  public static Promotion CreateEmpty() =>
    new(Guid.NewGuid(), 0, DateTime.MinValue, DateTime.MaxValue);

  public override IEnumerable<object> GetEqualityComponents()
  {
    yield return Id;
  }
}
