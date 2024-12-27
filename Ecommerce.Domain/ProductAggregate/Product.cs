namespace Ecommerce.Domain.ProductAggregate;

using Ecommerce.Domain.Common.Models;
using Ecommerce.Domain.Product.Entities;
using Ecommerce.Domain.ProductAggregate.ValueObjects;
using Ecommerce.Domain.UserAggregate.ValueObjects;

public sealed class Product : AggregateRoot<ProductId>, ITimeStamped
{
  public ProductName Name { get; private set; }
  public ProductDescription Description { get; private set; }
  public Price Price { get; private set; }
  public Stock Stock { get; private set; }
  public UserId SellerId { get; private set; }

  public IReadOnlyList<ProductCategory> Categories => _categories.AsReadOnly();
  public DateTime CreatedAt { get; private set; }
  public DateTime UpdatedAt { get; private set; }

  private readonly List<ProductCategory> _categories;

  private Product(
    ProductId productId,
    ProductName name,
    ProductDescription description,
    Price price,
    Stock stock,
    UserId sellerId,
    DateTime createdAt,
    DateTime updatedAt,
    List<ProductCategory> categories
  )
    : base(productId)
  {
    Id = productId;
    Name = name;
    Description = description;
    Price = price;
    Stock = stock;
    SellerId = sellerId;
    CreatedAt = createdAt;
    UpdatedAt = updatedAt;
    _categories = categories;
  }

  public static Product Create(
    ProductName name,
    ProductDescription description,
    Price price,
    Stock stock,
    UserId sellerId
  ) =>
    new(
      ProductId.CreateUnique(),
      name,
      description,
      price,
      stock,
      sellerId,
      DateTime.UtcNow,
      DateTime.UtcNow,
      []
    );

  public void UpdateName(ProductName name) => Name = name;

  public void UpdateDescription(ProductDescription description) => Description = description;

  public void UpdatePrice(Price price) => Price = price;

  public void UpdateStock(Stock stock) => Stock = stock;

  public void UpdateStockQuantity(Quantity quantity) => Stock.UpdateQuantity(quantity);

  public void UpdateStockReserved(int reserved) => Stock.UpdateReserved(reserved);

  public override IEnumerable<object> GetEqualityComponents()
  {
    yield return Id; // since its unique product, id will suffice
  }
}
