using Ecommerce.Domain.Common.Entities;
using Ecommerce.Domain.ProductAggregate.ValueObjects;

namespace Ecommerce.Domain.ProductAggregate.Entities;

public sealed class ProductImage : Image
{
  private ProductImage(Guid id, string url)
    : base(id, url, null, null, 0)
  {
    Url = url;
  }

  public static Image Create(string url)
  {
    return new ProductImage(Guid.NewGuid(), url);
  }
}
