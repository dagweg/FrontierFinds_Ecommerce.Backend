using Ecommerce.Domain.Common.Models;

namespace Ecommerce.Domain.ProductAggregate.ValueObjects;

public sealed class Quantity : ValueObject
{
  public int Value { get; private set; } = Empty;

  public static Quantity Empty => new(0);

  private Quantity() { }

  private Quantity(int value)
  {
    Value = value;
  }

  public static Quantity Create(int value)
  {
    return new(value);
  }

  public static implicit operator int(Quantity quantity) => quantity.Value;

  public override IEnumerable<object> GetEqualityComponents()
  {
    yield return Value;
  }
}
