namespace Ecommerce.Domain.ProductAggregate.ValueObjects;

using Ecommerce.Domain.Common.Models;

public sealed class ProductDescription : ValueObject
{
  public string Value { get; set; }

  public static ProductDescription Empty => new(string.Empty);

  public const int MIN_LENGTH = 100;
  public const int MAX_LENGTH = 2000;

  private ProductDescription(string value)
  {
    Value = value;
  }

  public static ProductDescription Create(string value)
  {
    return new(value);
  }

  public static implicit operator string(ProductDescription desc) => desc.Value;

  public override IEnumerable<object> GetEqualityComponents()
  {
    yield return Value;
  }
}
