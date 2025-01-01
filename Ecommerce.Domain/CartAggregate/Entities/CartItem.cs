using Ecommerce.Domain.CartAggregate.ValueObjects;
using Ecommerce.Domain.Common.Models;
using Ecommerce.Domain.ProductAggregate;

public sealed class CartItem : Entity<CartItemId>
{
  public int Quantity { get; private set; } = 0;

  public Product Product { get; private set; }

  public DateTime CreatedAt => DateTime.UtcNow;
  public DateTime UpdatedAt => DateTime.UtcNow;

  private CartItem(CartItemId id, Product product, int quantity)
    : base(id)
  {
    Product = product;
    Quantity = quantity;
  }

  public static CartItem Create(Product product, int quantity)
  {
    return new CartItem(CartItemId.CreateUnique(), product, quantity);
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
