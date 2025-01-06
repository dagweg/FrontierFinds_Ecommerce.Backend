using Ecommerce.Domain.Common.Models;

namespace Ecommerce.Domain.ProductAggregate.ValueObjects;

public sealed class Rating : ValueObject
{
  public decimal Value { get; set; }

  public static Rating Empty => new(0);

  private Rating() { }

  private Rating(decimal value)
  {
    if (value < 0 || value > 5)
    {
      throw new ArgumentOutOfRangeException(nameof(value), "Rating must be between 0 and 5.");
    }

    Value = value;
  }

  public static Rating Create(decimal value)
  {
    return new Rating(value);
  }

  public static implicit operator decimal(Rating rating) => rating.Value;

  public override IEnumerable<object> GetEqualityComponents()
  {
    yield return Value;
  }
}
