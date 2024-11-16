using Ecommerce.Domain.Common.Models;

namespace Ecommerce.Domain.Product.ValueObjects;

public sealed class ProductCategoryId : ValueObject
{
    private ProductCategoryId(Guid value)
    {
        Value = value;
    }

    private Guid Value { get; }

    public static ProductCategoryId Create()
    {
        return new ProductCategoryId(Guid.NewGuid());
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}