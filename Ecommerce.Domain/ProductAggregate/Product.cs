namespace Ecommerce.Domain.ProductAggregate;

using Ecommerce.Domain.Common.Entities;
using Ecommerce.Domain.Common.Models;
using Ecommerce.Domain.Common.ValueObjects;
using Ecommerce.Domain.ProductAggregate.Entities;
using Ecommerce.Domain.ProductAggregate.ValueObjects;
using Ecommerce.Domain.UserAggregate.ValueObjects;

public sealed class Product : AggregateRoot<ProductId>, ITimeStamped
{
  public ProductName Name { get; set; } = ProductName.Empty;
  public ProductDescription Description { get; set; } = ProductDescription.Empty;
  public Price Price { get; set; } = Price.Empty;
  public Stock Stock { get; set; } = Stock.Empty;
  public UserId SellerId { get; set; } = UserId.Empty;

  private readonly List<ProductCategory> _categories = [];
  private readonly List<ProductTag> _tags = [];
  private readonly List<ProductReview> _reviews = [];

  public IReadOnlyList<ProductCategory> Categories => _categories.AsReadOnly();
  public IReadOnlyList<ProductTag> Tags => _tags.AsReadOnly();
  public IReadOnlyList<ProductReview> Reviews => _reviews.AsReadOnly();

  public ProductImage Thumbnail { get; set; }
  public ProductImages Images { get; set; }
  public Rating AverageRating { get; set; }
  public Promotion Promotion { get; set; }

  public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
  public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

  private Product(
    ProductId productId,
    ProductName name,
    ProductDescription description,
    Price price,
    Stock stock,
    UserId sellerId,
    List<ProductCategory> categories,
    List<ProductTag> tags,
    List<ProductReview> reviews,
    ProductImage thumbnail,
    ProductImages productImages,
    Rating averageRating,
    Promotion promotion,
    DateTime createdAt,
    DateTime updatedAt
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
    _tags = tags;
    _reviews = reviews;

    Thumbnail = thumbnail;
    Images = productImages;
    AverageRating = averageRating;
    Promotion = promotion;
  }

  public static Product Create(
    ProductName name,
    ProductDescription description,
    Price price,
    Stock stock,
    UserId sellerId,
    List<ProductCategory> categories,
    List<ProductTag> tags,
    List<ProductReview> reviews,
    ProductImage thumbnail,
    ProductImages productImages,
    Rating averageRating,
    Promotion promotion
  ) =>
    new(
      ProductId.CreateUnique(),
      name,
      description,
      price,
      stock,
      sellerId,
      categories,
      tags,
      reviews,
      thumbnail,
      productImages,
      averageRating,
      promotion,
      DateTime.UtcNow,
      DateTime.UtcNow
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

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
  private Product()
    : base(ProductId.Empty) { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
}
