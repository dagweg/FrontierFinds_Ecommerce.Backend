using Ecommerce.Domain.ProductAggregate;

namespace Ecommerce.Application.Common.Models;

public class GetProductsResult : GetResult<Product>
{
  public required long MinPriceValueInCents { get; set; }
  public required long MaxPriceValueInCents { get; set; }
}
