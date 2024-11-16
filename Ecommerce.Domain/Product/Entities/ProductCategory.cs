using Ecommerce.Domain.Common.Models;
using Ecommerce.Domain.Product.ValueObjects;

namespace Ecommerce.Domain.Product.Entities;

public sealed class ProductCategory(ProductCategoryId id) : AggregateRoot<ProductCategoryId>(id), ITimeStamped
{
    public DateTime CreatedAt => DateTime.UtcNow;
    public DateTime UpdatedAt => DateTime.UtcNow;

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Id;
    }
}