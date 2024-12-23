namespace Ecommerce.Domain.Product.ValueObjects;

using Ecommerce.Domain.Common.Models;

public sealed class ProductCategoryId : ValueObject
{
  private ProductCategoryId(Guid value)
  {
    Value = value;
  }

  private Guid Value { get; }

  public static ProductCategoryId Create() => new ProductCategoryId(Guid.NewGuid());

  public override IEnumerable<object> GetEqualityComponents()
  {
    yield return Value;
  }
}
