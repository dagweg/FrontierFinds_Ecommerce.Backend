using Ecommerce.Domain.Common.Models;
using Ecommerce.Domain.OrderAggregate.Entities;
using Ecommerce.Domain.OrderAggregate.Enums;
using Ecommerce.Domain.OrderAggregate.ValueObjects;
using Ecommerce.Domain.UserAggregate.ValueObjects;

namespace Ecommerce.Domain.OrderAggregate;

public sealed class Order : AggregateRoot<OrderId>
{
  public UserId UserId { get; private set; } = UserId.Empty;
  private List<OrderItem> _orderItems = [];
  public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();

  public OrderStatus Status { get; private set; } = OrderStatus.None;

  public OrderTotal Total { get; private set; } = OrderTotal.Empty;
  public ShippingAddress ShippingAddress { get; private set; }
  public BillingAddress BillingAddress { get; private set; }

  public DateTime CreatedAt { get; private set; }
  public DateTime UpdatedAt { get; private set; }

  private Order(
    OrderId orderId,
    OrderStatus status,
    ShippingAddress shippingAddress,
    BillingAddress billingAddress,
    List<OrderItem> orderItems,
    DateTime createdAt,
    DateTime updatedAt
  )
    : base(orderId)
  {
    Status = status;
    ShippingAddress = shippingAddress;
    BillingAddress = billingAddress;
    _orderItems = orderItems;
    CreatedAt = createdAt;
    UpdatedAt = updatedAt;
  }

  public static Order Create(
    OrderStatus status,
    ShippingAddress shippingAddress,
    BillingAddress billingAddress,
    List<OrderItem> orderItems
  ) =>
    new(
      OrderId.CreateUnique(),
      status,
      shippingAddress,
      billingAddress,
      orderItems,
      DateTime.UtcNow,
      DateTime.UtcNow
    );

  public override IEnumerable<object> GetEqualityComponents()
  {
    yield return Id;
  }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
  private Order()
    : base(OrderId.Empty) { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
}
