using Ecommerce.Domain.Common.Models;

namespace Ecommerce.Domain.OrderAggregate.ValueObjects;

public sealed class OrderItemId : ValueObject
{
  public Guid Value { get; }

  public static OrderItemId Empty => new();

  private OrderItemId(Guid value)
  {
    Value = value;
  }

  public static OrderItemId CreateUnique() => new(Guid.NewGuid());

  public static OrderItemId Convert(Guid value) => new(value);

  public static implicit operator Guid(OrderItemId self) => self.Value;

  public override IEnumerable<object> GetEqualityComponents()
  {
    yield return Value;
  }

  private OrderItemId() { }
}
