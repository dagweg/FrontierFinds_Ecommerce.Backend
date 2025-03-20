using Ecommerce.Domain.Common.Models;

namespace Ecommerce.Domain.ProductAggregate.Entities;

public sealed class ProductTag : Entity<Guid>
{
  public string Name { get; set; } = string.Empty;

  private List<Product> _products = [];
  public IReadOnlyCollection<Product> Products => _products.AsReadOnly();

  private ProductTag(Guid id, string name)
    : base(id)
  {
    Name = name;
  }

  public static ProductTag Create(string name)
  {
    return new ProductTag(Guid.NewGuid(), name);
  }

  public override IEnumerable<object> GetEqualityComponents()
  {
    yield return Id;
  }
}
