using Ecommerce.Domain.Common.Models;
using Ecommerce.Domain.OrderAggregate.Entities;
using Ecommerce.Domain.OrderAggregate.Enums;
using Ecommerce.Domain.OrderAggregate.ValueObjects;
using Ecommerce.Domain.UserAggregate.ValueObjects;

namespace Ecommerce.Domain.OrderAggregate;

public sealed class Order : AggregateRoot<OrderId>
{
  public UserId UserId { get; set; } = UserId.Empty;
  public OrderTotal Total { get; set; }
  public ShippingAddress ShippingAddress { get; set; }
  public BillingAddress BillingAddress { get; set; }
  public OrderStatus Status { get; set; } = OrderStatus.None;

  private List<OrderItem> _orderItems = [];
  public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();
  public DateTime OrderDate { get; set; }

  private Order(
    OrderId orderId,
    OrderStatus status,
    OrderTotal total,
    ShippingAddress shippingAddress,
    BillingAddress billingAddress,
    List<OrderItem> orderItems,
    DateTime orderDate
  )
    : base(orderId)
  {
    Status = status;
    Total = total;
    ShippingAddress = shippingAddress;
    BillingAddress = billingAddress;
    _orderItems = orderItems;
    OrderDate = orderDate;
  }

  public static Order Create(
    OrderStatus status,
    OrderTotal total,
    ShippingAddress shippingAddress,
    BillingAddress billingAddress,
    List<OrderItem> orderItems
  ) =>
    new(
      OrderId.CreateUnique(),
      status,
      total,
      shippingAddress,
      billingAddress,
      orderItems,
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
