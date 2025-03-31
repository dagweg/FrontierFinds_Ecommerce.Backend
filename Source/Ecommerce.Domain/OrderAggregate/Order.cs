using Ecommerce.Domain.Common.Models;
using Ecommerce.Domain.OrderAggregate.Entities;
using Ecommerce.Domain.OrderAggregate.Enums;
using Ecommerce.Domain.OrderAggregate.Events;
using Ecommerce.Domain.OrderAggregate.ValueObjects;
using Ecommerce.Domain.UserAggregate.ValueObjects;

namespace Ecommerce.Domain.OrderAggregate;

public sealed class Order : AggregateRoot<OrderId>
{
  public UserId UserId { get; set; }
  public OrderTotal Total { get; set; }
  public ShippingAddress ShippingAddress { get; set; }
  public BillingAddress BillingAddress { get; set; }
  public OrderStatus Status { get; set; } = OrderStatus.Processing;

  private List<OrderItem> _orderItems = [];
  public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();
  public DateTime OrderDate { get; set; }

  private Order(
    UserId ordererId,
    OrderId orderId,
    List<OrderItem> orderItems,
    OrderTotal total,
    ShippingAddress shippingAddress,
    BillingAddress billingAddress,
    DateTime orderDate
  )
    : base(orderId)
  {
    UserId = ordererId;
    Total = total;
    ShippingAddress = shippingAddress;
    BillingAddress = billingAddress;
    _orderItems = orderItems;

    OrderDate = orderDate;
  }

  public static Order Create(
    UserId ordererId,
    List<OrderItem> orderItems,
    OrderTotal total,
    ShippingAddress shippingAddress,
    BillingAddress billingAddress
  )
  {
    Order order = new(
      ordererId,
      OrderId.CreateUnique(),
      orderItems,
      total,
      shippingAddress,
      billingAddress,
      // paymentInformation,
      DateTime.UtcNow
    );

    order.AddDomainEvent(new OrderCreatedDomainEvent((order)));

    return order;
  }

  public override IEnumerable<object> GetEqualityComponents()
  {
    yield return Id;
  }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
  private Order()
    : base(OrderId.Empty) { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
}
