using Ecommerce.Application.Common.Utilities;
using Ecommerce.Application.UseCases.Orders.Commands.CreateOrder;
using Ecommerce.Contracts.Order;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers;

[Authorize]
[ApiController]
[Route("orders")]
public class OrderController(ISender sender) : ControllerBase
{
  [HttpPost]
  public async Task<IActionResult> CreateOrder([FromBody] CreateOrderCommand createOrderRequest)
  {
    // new CreateOrderCommand
    // {
    //   OrderProducts = createOrderRequest.Products.Select(p => new OrderProductCommand
    //   {
    //     ProductId = p.ProductId,
    //     Quantity = p.Quantity,
    //   }),
    //   PaymentInformation = new PaymentInformationCommand
    //   {
    //     CardHolderName = createOrderRequest.PaymentInformation.CardHolderName,
    //     CardNumber = createOrderRequest.PaymentInformation.CardNumber,
    //     CVV = createOrderRequest.PaymentInformation.CVV,
    //     ExpiryMonth = createOrderRequest.PaymentInformation.ExpiryMonth,
    //     ExpiryYear = createOrderRequest.PaymentInformation.ExpiryYear,
    //   },
    //   ShippingAddress = new ShippingAddressCommand
    //   {
    //     City = createOrderRequest.ShippingAddress.City,
    //     Country = createOrderRequest.ShippingAddress.Country,
    //     State = createOrderRequest.ShippingAddress.State,
    //     Street = createOrderRequest.ShippingAddress.Street,
    //     ZipCode = createOrderRequest.ShippingAddress.ZipCode,
    //   },
    //   BillingAddress = new BillingAddressCommand
    //   {
    //     City = createOrderRequest.BillingAddress.City,
    //     Country = createOrderRequest.BillingAddress.Country,
    //     State = createOrderRequest.BillingAddress.State,
    //     Street = createOrderRequest.BillingAddress.Street,
    //     ZipCode = createOrderRequest.BillingAddress.ZipCode,
    //   },
    // }

    var result = await sender.Send(createOrderRequest);

    if (result.IsFailed)
      return new ObjectResult(result);

    LogPretty.Log(result);

    return Ok(result.Value);
  }
}
