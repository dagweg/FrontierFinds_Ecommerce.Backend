namespace Ecommerce.Domain.ProductAggregate.Entities;

using Ecommerce.Domain.Common.Entities;
using Ecommerce.Domain.Common.Models;
using Ecommerce.Domain.ProductAggregate.ValueObjects;

public class ProductCategory : Entity<Guid>
{
  public ProductId ProductId { get; private set; }
  public int CategoryId { get; private set; }

  public Product? Product { get; private set; }
  public Category? Category { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
  protected ProductCategory()
    : base(Guid.NewGuid()) { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

  public ProductCategory(ProductId productId, int categoryId)
    : base(Guid.NewGuid())
  {
    ProductId = productId;
    CategoryId = categoryId;
  }

  public override IEnumerable<object> GetEqualityComponents()
  {
    yield return Id;
  }
}
