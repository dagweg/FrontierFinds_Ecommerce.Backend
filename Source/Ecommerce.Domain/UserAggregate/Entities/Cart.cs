namespace Ecommerce.Domain.UserAggregate.Entities;

using Ecommerce.Domain.Common.Errors;
using Ecommerce.Domain.Common.Models;
using Ecommerce.Domain.ProductAggregate;
using Ecommerce.Domain.ProductAggregate.ValueObjects;
using Ecommerce.Domain.UserAggregate.ValueObjects;
using FluentResults;

public class Cart : Entity<CartId>
{
  private List<CartItem> _items = [];

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

  public void RemoveItem(CartItem cartItem)
  {
    _items.Remove(cartItem);
  }

  public void RemoveItems(HashSet<CartItemId> cartItemIds)
  {
    _items.RemoveAll(item => cartItemIds.Contains(item.Id));
  }

  public CartItem? GetItem(CartItemId cartItemId)
  {
    return _items.FirstOrDefault(i => i.Id == cartItemId);
  }

  public void ClearCart()
  {
    _items.Clear();
  }

  public Result AddItemsRange(
    List<CartItem> cartItems,
    IDictionary<ProductId, Product> productBulk,
    UserId userId
  )
  {
    foreach (var cartItem in cartItems)
    {
      if (
        productBulk.ContainsKey(cartItem.ProductId)
        && productBulk[cartItem.ProductId].SellerId == userId
      )
      {
        return InvalidOperationError.GetResult(
          nameof(cartItem.ProductId),
          "You cannot add your own product to cart"
        );
      }

      var existingCartItem = _items.FirstOrDefault(item => item.ProductId == cartItem.ProductId);

      if (existingCartItem != null)
      {
        // Item already exists in cart, update quantity
        var availableStockQuantity = productBulk.ContainsKey(cartItem.ProductId)
          ? productBulk[cartItem.ProductId].Stock.Quantity
          : 0; // Handle case if product is not in bulk (though it should be)
        var requestedQuantity = existingCartItem.Quantity + cartItem.Quantity;

        existingCartItem.SetQuantity(requestedQuantity, availableStockQuantity); // Use SetQuantity to handle stock limits if needed
      }
      else
      {
        // Item does not exist in cart, add it
        _items.Add(cartItem);
      }
    }

    return Result.Ok();
  }

  public void UpdateCartItems(IEnumerable<CartItem> cartItems)
  {
    _items = cartItems.ToList();
  }

  public void UpdateCartItem(CartItem cartItem)
  {
    var item = _items.FirstOrDefault(i => i.Id == cartItem.Id);

    if (item is null)
    {
      return;
    }
    _items.Remove(item);
    _items.Add(cartItem);
  }

  public Result DecrementItemQuantities(Dictionary<CartItemId, int> cartItems)
  {
    foreach (var cartItem in cartItems)
    {
      var item = _items.FirstOrDefault(i => i.Id == cartItem.Key);

      if (item is null)
      {
        continue;
      }

      item.DecreaseQuantity(cartItem.Value);
    }

    return Result.Ok();
  }

  public override IEnumerable<object> GetEqualityComponents()
  {
    yield return Id;
  }
}
