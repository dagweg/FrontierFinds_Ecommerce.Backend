using Ecommerce.Domain.Common.Entities;
using Ecommerce.Domain.ProductAggregate.Enums;
using Ecommerce.Domain.ProductAggregate.ValueObjects;

namespace Ecommerce.Domain.ProductAggregate.Entities;

public sealed class ProductImage
{
  public string Url { get; private set; }
  public string ObjectIdentifier { get; private set; }

  private ProductImage(string url, string objectIdentifier)
  {
    Url = url;
    ObjectIdentifier = objectIdentifier;
  }

  public static ProductImage Create(string url, string objectIdentifier)
  {
    return new ProductImage(url, objectIdentifier);
  }
}
