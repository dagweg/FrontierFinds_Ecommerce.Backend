namespace Ecommerce.Domain.ProductAggregate.Entities;

using Ecommerce.Domain.Common.Models;
using Ecommerce.Domain.ProductAggregate.ValueObjects;

public sealed class ProductCategory : Entity<Guid>
{
  public ProductCategoryName Name { get; private set; } = ProductCategoryName.Empty;

  private ProductCategory()
    : base(Guid.Empty) { }

  private ProductCategory(Guid id, ProductCategoryName name)
    : base(id)
  {
    Name = name;
  }

  public static ProductCategory Create(ProductCategoryName name)
  {
    return new ProductCategory(Guid.NewGuid(), name);
  }

  public static implicit operator string(ProductCategory category) => category.Name;

  public override IEnumerable<object> GetEqualityComponents()
  {
    yield return Id;
  }
}
