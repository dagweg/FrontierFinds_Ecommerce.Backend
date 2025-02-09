using Ecommerce.Domain.Common.Models;
using Ecommerce.Domain.ProductAggregate.ValueObjects;

namespace Ecommerce.Domain.UserAggregate.Entities;

public sealed class Wishlist : Entity<Guid>
{
  private List<ProductId> _productIds = [];

  public IReadOnlyCollection<ProductId> ProductIds => _productIds.AsReadOnly();

  private Wishlist(Guid id, List<ProductId> productIds)
    : base(id)
  {
    Id = id;
    _productIds = productIds;
  }

  public static Wishlist Create(List<ProductId>? productIds = null)
  {
    return new Wishlist(Guid.NewGuid(), productIds ?? []);
  }

  public void AddProductsRange(List<ProductId> productIds)
  {
    _productIds.AddRange(productIds);
    _productIds = _productIds.Distinct().ToList();
  }

  public void RemoveProductsRange(HashSet<ProductId> productIds)
  {
    _productIds.RemoveAll(p => productIds.Contains(p));
  }

  public override IEnumerable<object> GetEqualityComponents()
  {
    yield return Id;
  }

  private Wishlist()
    : base(Guid.Empty) { }
}
