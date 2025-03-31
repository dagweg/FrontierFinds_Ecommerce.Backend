using Ecommerce.Application.Common.Models;
using Ecommerce.Domain.OrderAggregate;
using Ecommerce.Domain.OrderAggregate.ValueObjects;
using Ecommerce.Domain.UserAggregate.ValueObjects;

namespace Ecommerce.Application.Common.Interfaces.Persistence;

public interface IOrderRepository : IRepository<Order, OrderId>
{
  /// <summary>
  /// Gets the orders by user identifier asynchronous.
  /// </summary>
  /// <param name="userId"></param>
  /// <returns>OrdersResult otherwise null (if user doesn't exist)</returns>
  Task<GetOrdersResult?> GetOrdersByUserIdAsync(
    UserId userId,
    PaginationParameters paginationParameters
  );
}
