using Ecommerce.Domain.Common.Enums;
using Ecommerce.Domain.Common.Models;

namespace Ecommerce.Domain.OrderAggregate.ValueObjects;

public sealed class OrderTotal : ValueObject
{
  public decimal Value { get; private set; }
  public Currency Currency { get; private set; }

  public static OrderTotal Empty => new(0);

  private OrderTotal(decimal value)
  {
    Value = value;
  }

  public static OrderTotal Create(decimal value)
  {
    return new(value);
  }

  public static implicit operator decimal(OrderTotal total) => total.Value;

  public override IEnumerable<object> GetEqualityComponents()
  {
    yield return Value;
  }

  private OrderTotal() { }
}
