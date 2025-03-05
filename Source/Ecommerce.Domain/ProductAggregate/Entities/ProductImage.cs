using Ecommerce.Domain.Common.Entities;
using Ecommerce.Domain.ProductAggregate.Enums;
using Ecommerce.Domain.ProductAggregate.ValueObjects;

namespace Ecommerce.Domain.ProductAggregate.Entities;

public sealed class ProductImage : Image
{
  private ProductImage(string url, string objectIdentifier)
    : base(url, objectIdentifier) { }

  public static new ProductImage Create(string url, string objectIdentifier)
  {
    return new ProductImage(url, objectIdentifier);
  }
}
