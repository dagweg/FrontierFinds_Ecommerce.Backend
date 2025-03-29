using Ecommerce.Application.Common.Models;

namespace Ecommerce.Application.Common.Extensions;

public static class PaginationExtension
{
  public static IQueryable<T> Paginate<T>(
    this IQueryable<T> items,
    PaginationParameters? paginationParameters
  )
  {
    if (paginationParameters is null)
      return items;
    return items
      .Skip((paginationParameters.PageNumber - 1) * paginationParameters.PageSize)
      .Take(paginationParameters.PageSize);
  }
}
