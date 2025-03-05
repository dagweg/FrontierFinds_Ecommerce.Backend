using Ecommerce.Domain.Common.Models;
using Ecommerce.Domain.Common.ValueObjects;
using Ecommerce.Domain.OrderAggregate.ValueObjects;
using Ecommerce.Domain.ProductAggregate.ValueObjects;

namespace Ecommerce.Domain.OrderAggregate.Entities;

public sealed class OrderItem : Entity<OrderItemId>
{
  public ProductId ProductId { get; set; }
  public int Quantity { get; set; }
  public Price Price { get; set; }

  private OrderItem(OrderItemId orderItemId, ProductId productId, int quantity, Price price)
    : base(orderItemId)
  {
    ProductId = productId;
    Quantity = quantity;
    Price = price;
  }

  public static OrderItem Create(ProductId productId, int quantity, Price price)
  {
    return new OrderItem(OrderItemId.CreateUnique(), productId, quantity, price);
  }

  public override IEnumerable<object> GetEqualityComponents()
  {
    yield return Id;
  }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
  private OrderItem()
    : base(OrderItemId.Empty) { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
}
