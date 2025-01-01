namespace Ecommerce.Domain.UserAggregate.Entities;

using Ecommerce.Domain.Common.Models;
using Ecommerce.Domain.ProductAggregate;
using Ecommerce.Domain.ProductAggregate.ValueObjects;
using Ecommerce.Domain.UserAggregate.ValueObjects;

public sealed class CartItem : Entity<CartItemId>
{
  public int Quantity { get; private set; }

  public ProductId ProductId { get; private set; }

  private CartItem(CartItemId id, ProductId productId, int quantity)
    : base(id)
  {
    ProductId = productId;
    Quantity = quantity;
  }

  public static CartItem Create(ProductId productId, int quantity)
  {
    return new CartItem(CartItemId.CreateUnique(), productId, quantity);
  }

  public void IncreaseQuantity(int quantity)
  {
    Quantity += quantity;
  }

  public void DecreaseQuantity(int quantity)
  {
    Quantity -= quantity;
  }

  public override IEnumerable<object> GetEqualityComponents()
  {
    yield return Id;
  }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
  private CartItem()
    : base(CartItemId.Empty) { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
}
