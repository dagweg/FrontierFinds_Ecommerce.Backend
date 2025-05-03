using Ecommerce.Application.Common.Utilities;
using Ecommerce.Application.UseCases.Orders.Commands.CreateOrder;
using Ecommerce.Application.UseCases.Orders.Queries.GetOrders;
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
    var result = await sender.Send(createOrderRequest);

    if (result.IsFailed)
      return new ObjectResult(result);

    LogPretty.Log(result);

    return Ok(result.Value);
  }

  [HttpGet]
  public async Task<IActionResult> GetOrders()
  {
    var result = await sender.Send(new GetOrdersCommand());

    if (result.IsFailed)
      return new ObjectResult(result);

    return Ok(result.Value);
  }
}
