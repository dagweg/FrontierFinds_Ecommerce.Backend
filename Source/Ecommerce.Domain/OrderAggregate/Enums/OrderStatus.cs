namespace Ecommerce.Domain.OrderAggregate.Enums;

// (processing -> success -> shipped )| cancelled| failed
public enum OrderStatus
{
  Failed = 0,
  Processing,
  Success,
  Shipped,
  Cancelled,
}
