using Ecommerce.Domain.Common.Models;

namespace Ecommerce.Domain.OrderAggregate.ValueObjects;

public sealed class OrderId : ValueObject
{
  public Guid Value { get; }

  public static OrderId Empty => new();

  private OrderId(Guid value)
  {
    Value = value;
  }

  public static OrderId CreateUnique() => new(Guid.NewGuid());

  public static OrderId Convert(Guid value) => new(value);

  public static implicit operator Guid(OrderId self) => self.Value;

  public override IEnumerable<object> GetEqualityComponents()
  {
    yield return Value;
  }

  private OrderId() { }
}
