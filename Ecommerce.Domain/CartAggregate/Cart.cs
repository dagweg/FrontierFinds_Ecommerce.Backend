using Ecommerce.Domain.CartAggregate.ValueObjects;
using Ecommerce.Domain.Common.Models;

namespace Ecommerce.Domain.CartAggregate;

public class Cart : AggregateRoot<CartId>
{
  private readonly List<CartItem> _items = [];

  public IReadOnlyList<CartItem> Items => _items.AsReadOnly();

  private Cart()
    : base(CartId.CreateUnique()) { }

  private Cart(CartId cartId)
    : base(cartId) { }

  public static Cart Create(CartId cartId)
  {
    return new Cart(cartId);
  }

  public override IEnumerable<object> GetEqualityComponents()
  {
    yield return Id;
  }
}
