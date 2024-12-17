namespace Ecommerce.Domain.Product.ValueObjects;

using Ecommerce.Domain.Common.Models;

public sealed class ProductId : ValueObject
{
    private ProductId(Guid value)
    {
        Value = value;
    }

    public Guid Value { get; }

    public static ProductId CreateUnique() => new ProductId(Guid.NewGuid());

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
