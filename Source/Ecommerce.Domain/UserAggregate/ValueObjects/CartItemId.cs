using Ecommerce.Domain.Common.Models;

namespace Ecommerce.Domain.UserAggregate.ValueObjects;

public sealed class CartItemId : ValueObject
{
  public Guid Value { get; set; }

  public static CartItemId Empty => new CartItemId(Guid.Empty);

  private CartItemId() { }

  private CartItemId(Guid value)
  {
    Value = value;
  }

  public static CartItemId CreateUnique()
  {
    return new CartItemId(Guid.NewGuid());
  }

  public static CartItemId Convert(Guid value)
  {
    return new CartItemId(value);
  }

  public static implicit operator string(CartItemId cartItemId) => cartItemId.Value.ToString();

  public static implicit operator Guid(CartItemId cartItemId) => cartItemId.Value;

  public override IEnumerable<object> GetEqualityComponents()
  {
    yield return Value;
  }
}
