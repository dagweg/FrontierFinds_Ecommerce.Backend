using Ecommerce.Domain.Common.Models;
using Ecommerce.Domain.OrderAggregate.Entities;
using Ecommerce.Domain.OrderAggregate.Enums;
using Ecommerce.Domain.OrderAggregate.Events;
using Ecommerce.Domain.OrderAggregate.ValueObjects;
using Ecommerce.Domain.UserAggregate.ValueObjects;

namespace Ecommerce.Domain.OrderAggregate;

public sealed class Order : AggregateRoot<OrderId>
{
    public UserId UserId { get; set; } = UserId.Empty;
    public OrderTotal Total { get; set; }
    public ShippingAddress ShippingAddress { get; set; }
    public BillingAddress BillingAddress { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Processing;

    private List<OrderItem> _orderItems = [];
    public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();
    public DateTime OrderDate { get; set; }

    public PaymentInformation PaymentInformation { get; set; }

    private Order(
      OrderId orderId,
      List<OrderItem> orderItems,
      OrderTotal total,
      ShippingAddress shippingAddress,
      BillingAddress billingAddress,
      PaymentInformation paymentInformation,
      DateTime orderDate
    )
      : base(orderId)
    {
        Total = total;
        ShippingAddress = shippingAddress;
        BillingAddress = billingAddress;
        _orderItems = orderItems;
        PaymentInformation = paymentInformation;
        OrderDate = orderDate;
    }

    public static Order Create(
      List<OrderItem> orderItems,
      OrderTotal total,
      ShippingAddress shippingAddress,
      BillingAddress billingAddress,
      PaymentInformation paymentInformation
    )
    {
        Order order = new(
          OrderId.CreateUnique(),
          orderItems,
          total,
          shippingAddress,
          billingAddress,
          paymentInformation,
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
