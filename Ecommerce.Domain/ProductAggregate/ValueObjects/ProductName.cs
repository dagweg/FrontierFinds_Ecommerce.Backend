namespace Ecommerce.Domain.ProductAggregate.ValueObjects;

using Ecommerce.Domain.Common.Models;

public sealed class ProductName : ValueObject
{
  public string Value { get; set; } = Empty;

  public static ProductName Empty => new(string.Empty);

  private ProductName() { }

  private ProductName(string name)
  {
    Value = name;
  }

  public static ProductName Create(string name) => new(name);

  public static implicit operator string(ProductName name) => name.Value;

  public override IEnumerable<object> GetEqualityComponents()
  {
    yield return Value;
  }
}
