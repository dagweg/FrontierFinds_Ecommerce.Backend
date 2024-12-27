namespace Ecommerce.Domain.ProductAggregate.ValueObjects;

using Ecommerce.Domain.Common.Models;

public sealed class ProductCategoryName : ValueObject
{
  public string Value { get; set; } = Empty;
  public static ProductCategoryName Empty => new(string.Empty);

  private ProductCategoryName() { }

  private ProductCategoryName(string name)
  {
    Value = name;
  }

  public static ProductCategoryName Create(string name) => new(name);

  public static implicit operator string(ProductCategoryName name) => name.Value;

  public override IEnumerable<object> GetEqualityComponents()
  {
    yield return Value;
  }
}
