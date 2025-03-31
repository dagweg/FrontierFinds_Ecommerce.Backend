using Ecommerce.Application.Common.Interfaces.Providers.Payment.Stripe;
using FluentResults;
using MediatR;

namespace Ecommerce.Application.UseCases.Orders.Commands.CreateOrder;

public class CreateOrderCommand : IRequest<Result<CreateCheckoutSessionResult>>
{
  public required IEnumerable<OrderProductCommand> OrderProducts { get; set; }
  public required ShippingAddressCommand ShippingAddress { get; set; }
  public required BillingAddressCommand BillingAddress { get; set; }
  // public required PaymentInformationCommand PaymentInformation { get; set; }
}
