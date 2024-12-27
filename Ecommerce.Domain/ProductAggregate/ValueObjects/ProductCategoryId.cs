namespace Ecommerce.Domain.ProductAggregate.ValueObjects;

using Ecommerce.Domain.Common.Models;

public sealed class ProductCategoryId : ValueObject
{
  public Guid Value { get; } = Empty;

  public static ProductCategoryId Empty { get; } = new(Guid.Empty);

  private ProductCategoryId() { }

  private ProductCategoryId(Guid value)
  {
    Value = value;
  }

  public static ProductCategoryId CreateUnique() => new(Guid.NewGuid());

  public static implicit operator Guid(ProductCategoryId id) => id.Value;

  public override IEnumerable<object> GetEqualityComponents()
  {
    yield return Value;
  }
}
