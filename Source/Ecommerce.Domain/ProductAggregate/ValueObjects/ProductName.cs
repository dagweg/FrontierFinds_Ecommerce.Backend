namespace Ecommerce.Domain.ProductAggregate.ValueObjects;

using Ecommerce.Domain.Common.Models;

public sealed class ProductName : ValueObject
{
    public string Value { get; set; }

    public static ProductName Empty => new(string.Empty);

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private ProductName() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

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
