namespace Ecommerce.Domain.UserAggregate.Entities;

using Ecommerce.Domain.Common.Models;
using Ecommerce.Domain.UserAggregate.ValueObjects;

public class Cart : Entity<CartId>
{
    private readonly List<CartItem> _items = [];

    public IReadOnlyList<CartItem> Items => _items.AsReadOnly();

    private Cart()
      : base(CartId.CreateUnique()) { }

    private Cart(CartId cartId)
      : base(cartId) { }

    public static Cart Create()
    {
        return new Cart(CartId.CreateUnique());
    }

    public void AddItem(CartItem cartItem)
    {
        _items.Add(cartItem);
    }

    public void RemoveItems(HashSet<CartItemId> cartItemIds)
    {
        _items.RemoveAll(item => cartItemIds.Contains(item.Id));
    }

    public void ClearCart()
    {
        _items.Clear();
    }

    public void AddItemsRange(List<CartItem> cartItems)
    {
        _items.AddRange(cartItems);
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Id;
    }
}
