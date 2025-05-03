using Ecommerce.Application.Common.Extensions;
using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.Common.Models;
using Ecommerce.Application.Common.Utilities;
using Ecommerce.Domain.OrderAggregate;
using Ecommerce.Domain.OrderAggregate.Entities;
using Ecommerce.Domain.OrderAggregate.ValueObjects;
using Ecommerce.Domain.ProductAggregate;
using Ecommerce.Domain.ProductAggregate.ValueObjects;
using Ecommerce.Domain.UserAggregate.ValueObjects;
using Ecommerce.Infrastructure.Common.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Persistence.EfCore.Repositories;

public class OrderRepository : EfCoreRepository<Order, OrderId>, IOrderRepository
{
  private readonly EfCoreContext _context;
  private readonly DbSet<Order> _orders;
  private readonly IProductRepository _pr;

  public OrderRepository(EfCoreContext context, IProductRepository pr)
    : base(context)
  {
    _context = context;
    _orders = _context.Orders;
    _pr = pr;
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

    var mappedOrderIdWithProducts = _pr.GetMappedOrderIdWithProducts(
      paginated.ToDictionary(kvp => kvp.Id, kvp => kvp.OrderItems.Select(x => x.ProductId))
    );

    // LogPretty.Log(mappedOrderIdWithProducts);

    return new GetOrdersResult
    {
      TotalItems = orders.Count(),
      TotalItemsFetched = paginated.Count(),
      Items = paginated.Select(o =>
      {
        // LogPretty.Log("Order Id: " + o.Id.Value);
        // LogPretty.Log(
        //   "\nOrder Items Product: "
        //     + string.Join("\n,", mappedOrderIdWithProducts[o.Id].Select(p => p.Name.Value))
        // );
        return (o, mappedOrderIdWithProducts[o.Id]);
      }),
    };
  }
}
