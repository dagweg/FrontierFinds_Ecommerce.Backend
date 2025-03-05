using Ecommerce.Domain.Common.Models;

namespace Ecommerce.Domain.ProductAggregate.Entities;

public sealed class ProductTag : Entity<Guid>
{
    public string Name { get; set; } = string.Empty;

    private ProductTag(Guid id, string name)
      : base(id)
    {
        Name = name;
    }

    public static ProductTag Create(Guid id, string name)
    {
        var productTag = new ProductTag(id, name);
        return productTag;
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Id;
    }
}
