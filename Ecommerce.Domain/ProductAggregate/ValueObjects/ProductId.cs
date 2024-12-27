namespace Ecommerce.Domain.ProductAggregate.ValueObjects;

using Ecommerce.Domain.Common.Models;

public sealed class ProductId : ValueObject
{
  public Guid Value { get; } = Empty;

  public static ProductId Empty => new(Guid.Empty);

  private ProductId(Guid value)
  {
    Value = value;
  }

  public static ProductId CreateUnique() => new(Guid.NewGuid());

  public static implicit operator Guid(ProductId id) => id.Value;

  public override IEnumerable<object> GetEqualityComponents()
  {
    yield return Value;
  }
}
