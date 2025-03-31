using Ecommerce.Application.Common.Extensions;
using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.Common.Models;
using Ecommerce.Application.Common.Utilities;
using Ecommerce.Domain.OrderAggregate;
using Ecommerce.Domain.OrderAggregate.ValueObjects;
using Ecommerce.Domain.UserAggregate.ValueObjects;
using Ecommerce.Infrastructure.Common.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Persistence.EfCore.Repositories;

public class OrderRepository : EfCoreRepository<Order, OrderId>, IOrderRepository
{
  private readonly EfCoreContext _context;
  private readonly DbSet<Order> _orders;

  public OrderRepository(EfCoreContext context)
    : base(context)
  {
    _context = context;
    _orders = _context.Orders;
  }

  public async Task<GetOrdersResult?> GetOrdersByUserIdAsync(
    UserId userId,
    PaginationParameters paginationParameters
  )
  {
    var orders = _orders.Where(x => x.UserId == userId);

    if (orders is null)
      return null;

    var paginated = await orders
      .Paginate(paginationParameters)
      .Include(x => x.OrderItems)
      .ToListAsync();

    LogPretty.Log(paginated);

    return new GetOrdersResult
    {
      TotalItems = orders.Count(),
      TotalItemsFetched = paginated.Count(),
      Items = paginated,
    };
  }
}
