using System.Collections.Immutable;
using AutoMapper;
using Ecommerce.Application.Common.Errors;
using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.Common.Interfaces.Providers.Context;
using Ecommerce.Application.Common.Models;
using Ecommerce.Application.Common.Utilities;
using Ecommerce.Application.UseCases.Orders.Common;
using Ecommerce.Application.UseCases.Products.Common;
using Ecommerce.Domain.Common.Enums;
using Ecommerce.Domain.OrderAggregate;
using Ecommerce.Domain.OrderAggregate.Entities;
using Ecommerce.Domain.ProductAggregate;
using FluentResults;
using MediatR;

namespace Ecommerce.Application.UseCases.Orders.Queries.GetOrders;

public class GetOrdersCommandHandler(
  IOrderRepository orderRepository,
  IUserContextService context,
  IMapper mapper
) : IRequestHandler<GetOrdersCommand, Result<OrdersResult>>
{
  public async Task<Result<OrdersResult>> Handle(
    GetOrdersCommand request,
    CancellationToken cancellationToken
  )
  {
    var userId = context.GetValidUserId();

    if (userId.IsFailed)
      return userId.ToResult();

    var orders = await orderRepository.GetOrdersByUserIdAsync(
      userId.Value,
      new PaginationParameters { PageNumber = request.PageNumber, PageSize = request.PageSize }
    );

    if (orders is null)
      return NotFoundError.GetResult("user", "user is not found");

    // LogPretty.Log(orders);

    var res = new OrdersResult
    {
      Items = orders
        .Items.Select(tup => new OrderResult
        {
          OrderId = tup.order.Id.Value.ToString(),
          OrderDate = tup.order.OrderDate,
          ShippingAddress = tup.order.ShippingAddress,
          Status = tup.order.Status.ToString(),
          Total = new OrderTotalResult()
          {
            Currency = Currency.USD.ToString(),
            ValueTotalInCents = tup.products.Sum(p =>
            {
              var oi = tup.order.OrderItems.Where(oi => oi.ProductId == p.Id).First();
              long sum = p.Price.ValueInCents * (oi is null ? 0 : oi.Quantity);

              return sum;
            }),
          },
          UserId = tup.order.UserId,
          OrderItems = tup
            .products.Select(p =>
            {
              var oi = tup.order.OrderItems.FirstOrDefault(oi => oi.ProductId == p.Id);
              var res = new OrderItemResult
              {
                OrderItemTotalInCents = p.Price.ValueInCents * (oi is null ? 0 : oi.Quantity),
                Product = mapper.Map<ProductResult>(p),
                Quantity = oi is null ? 0 : oi.Quantity,
              };
              // Console.WriteLine("Order Item Result\n:");
              // LogPretty.Log(res); // Order Item is printing okay!!!
              return res;
            })
            .ToList(),
        })
        .ToList(),
      TotalItems = orders.TotalItems,
      TotalItemsFetched = orders.TotalItemsFetched,
    };

    // WHAT IN THE BLOODY HELL IS HAPPENING, Order Items are not being included in the 'res'??? Even though they're printed
    // and are present when constructing the dto above

    // LogPretty.Log(res.Items.Select(x => x.OrderItems)); // Order Item is not Present Here! In the result object

    return res;
  }
}
