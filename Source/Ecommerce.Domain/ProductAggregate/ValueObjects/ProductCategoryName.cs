namespace Ecommerce.Domain.ProductAggregate.ValueObjects;

using Ecommerce.Domain.Common.Models;

public sealed class ProductCategoryName : ValueObject
{
  public string Value { get; set; }
  public static ProductCategoryName Empty => new(string.Empty);

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
  private ProductCategoryName() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

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
