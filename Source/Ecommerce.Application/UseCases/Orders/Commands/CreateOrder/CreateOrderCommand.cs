using FluentResults;
using MediatR;

namespace Ecommerce.Application.UseCases.Orders.Commands.CreateOrder;

public class CreateOrderCommand : IRequest<Result>
{
  public required IEnumerable<OrderProductCommand> OrderProducts { get; set; }
  public required ShippingAddressCommand ShippingAddress { get; set; }
  public required PaymentInformationCommand PaymentInformation { get; set; }
}
