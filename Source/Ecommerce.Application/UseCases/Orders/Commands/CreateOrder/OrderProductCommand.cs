namespace Ecommerce.Application.UseCases.Orders.Commands.CreateOrder;

public class OrderProductCommand
{
  public required string ProductId { get; set; }
  public required int Quantity { get; set; }
}
