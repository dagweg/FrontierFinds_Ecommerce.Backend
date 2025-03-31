using Ecommerce.Domain.OrderAggregate;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Common.Extensions;

public static class OrderQueryExtensions
{
  public static IQueryable<Order> IncludeEverything(this IQueryable<Order> query)
  {
    return query.Include(x => x.OrderItems);
  }
}
