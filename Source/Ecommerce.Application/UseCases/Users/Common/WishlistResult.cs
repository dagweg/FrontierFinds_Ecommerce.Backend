using Ecommerce.Application.UseCases.Products.Common;
using Ecommerce.Domain.ProductAggregate;

namespace Ecommerce.Application.UseCases.Users.Common;

public class WishlistResult
{
  public required ProductResult Product { get; set; }
}
