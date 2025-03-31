using AutoMapper;
using Ecommerce.Application.Common.Errors;
using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.Common.Interfaces.Providers.Context;
using Ecommerce.Application.Common.Models;
using Ecommerce.Application.Common.Utilities;
using Ecommerce.Application.UseCases.Orders.Common;
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

    return mapper.Map<OrdersResult>(orders);
  }
}
