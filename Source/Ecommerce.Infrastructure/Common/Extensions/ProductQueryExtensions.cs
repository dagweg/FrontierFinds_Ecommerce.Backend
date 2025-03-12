using Ecommerce.Domain.ProductAggregate;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure.Common.Extensions;

public static class ProductQueryExtensions
{
  public static IQueryable<Product> IncludeEverything(this IQueryable<Product> query)
  {
    return query.Include(p => p.Images).Include(p => p.Tags);
  }
}
