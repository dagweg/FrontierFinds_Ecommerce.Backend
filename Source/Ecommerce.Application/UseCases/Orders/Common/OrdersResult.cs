using Ecommerce.Domain.OrderAggregate.ValueObjects;

namespace Ecommerce.Application.UseCases.Orders.Common;

public class OrdersResult
{
  public int TotalItems { get; set; } = 0;
  public int TotalItemsFetched { get; set; } = 0;
  public List<OrderResult> Items { get; set; } = new List<OrderResult>();
}

public class OrderResult
{
  public required string OrderId { get; set; }
  public required string UserId { get; set; }
  public required OrderTotalResult Total { get; set; }
  public required ShippingAddress ShippingAddress { get; set; }
  public required string Status { get; set; }

  public List<OrderItemResult> OrderItems = [];
  public required DateTime OrderDate { get; set; }
}

public class OrderTotalResult
{
  public required long ValueTotalInCents { get; set; }
  public required string Currency { get; set; }
}

public class OrderItemResult
{
  public required string ProductId { get; set; }
  public required int Quantity { get; set; }
  public required long PriceValueInCents { get; set; }
}
