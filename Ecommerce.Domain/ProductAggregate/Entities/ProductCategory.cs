namespace Ecommerce.Domain.Product.Entities;

using Ecommerce.Domain.Common.Models;
using Ecommerce.Domain.ProductAggregate.ValueObjects;

public sealed class ProductCategory : AggregateRoot<ProductCategoryId>, ITimeStamped
{
  public ProductCategoryName Name { get; private set; }

  public DateTime CreatedAt => DateTime.UtcNow;
  public DateTime UpdatedAt => DateTime.UtcNow;

  private ProductCategory(ProductCategoryId id, ProductCategoryName name)
    : base(id)
  {
    Name = name;
  }

  public static ProductCategory Create(ProductCategoryName name)
  {
    return new ProductCategory(ProductCategoryId.CreateUnique(), name);
  }

  public override IEnumerable<object> GetEqualityComponents()
  {
    yield return Id;
  }
}
