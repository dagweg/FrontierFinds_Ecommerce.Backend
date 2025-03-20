namespace Ecommerce.Domain.UserAggregate.Entities;

using Ecommerce.Domain.Common.Errors;
using Ecommerce.Domain.Common.Models;
using Ecommerce.Domain.ProductAggregate;
using Ecommerce.Domain.ProductAggregate.ValueObjects;
using Ecommerce.Domain.UserAggregate.ValueObjects;
using FluentResults;

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

  public Result AddItemsRange(
    List<CartItem> cartItems,
    IDictionary<ProductId, Product> productBulk,
    UserId userId
  )
  {
    var cartDict = Items.ToDictionary(kvp => kvp.ProductId, kvp => kvp);

    var newCartItems = new List<CartItem>();

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
      if (cartDict.ContainsKey(cartItem.ProductId) && productBulk.ContainsKey(cartItem.ProductId))
      {
        var availableStockQuantity = productBulk[cartItem.ProductId].Stock.Quantity;
        var requestedQuantity = cartDict[cartItem.ProductId].Quantity + cartItem.Quantity;

        if (requestedQuantity <= availableStockQuantity)
          cartDict[cartItem.ProductId].SetQuantity(requestedQuantity);
        else
          cartDict[cartItem.ProductId].SetQuantity(availableStockQuantity);

        newCartItems.Add(cartDict[cartItem.ProductId]);
      }
      else
      {
        newCartItems.Add(cartItem);
      }
    }
    _items.AddRange(newCartItems);

    return Result.Ok();
  }

  public override IEnumerable<object> GetEqualityComponents()
  {
    yield return Id;
  }
}
