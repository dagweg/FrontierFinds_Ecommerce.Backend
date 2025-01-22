namespace Ecommerce.Domain.ProductAggregate;

using Ecommerce.Domain.Common.Entities;
using Ecommerce.Domain.Common.Models;
using Ecommerce.Domain.Common.ValueObjects;
using Ecommerce.Domain.ProductAggregate.Entities;
using Ecommerce.Domain.ProductAggregate.ValueObjects;
using Ecommerce.Domain.UserAggregate.ValueObjects;

public sealed class Product : AggregateRoot<ProductId>
{
  public ProductName Name { get; private set; } = ProductName.Empty;
  public ProductDescription Description { get; private set; } = ProductDescription.Empty;
  public Price Price { get; private set; } = Price.Empty;
  public Stock Stock { get; private set; } = Stock.Empty;
  public UserId SellerId { get; private set; } = UserId.Empty;
  public ProductImage Thumbnail { get; private set; }

  private readonly List<ProductCategory> _categories = [];
  private readonly List<ProductTag> _tags = [];
  private readonly List<ProductReview> _reviews = [];

  public IReadOnlyList<ProductCategory> Categories => _categories.AsReadOnly();
  public IReadOnlyList<ProductTag> Tags => _tags.AsReadOnly();
  public IReadOnlyList<ProductReview> Reviews => _reviews.AsReadOnly();

  public ProductImages Images { get; private set; }
  public Rating AverageRating { get; private set; }
  public Promotion Promotion { get; private set; }

  private Product(
    ProductId productId,
    ProductName name,
    ProductDescription description,
    Price price,
    Stock stock,
    UserId sellerId,
    ProductImage thumbnail,
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
    Thumbnail = thumbnail;
    Promotion = Promotion.Create(0, DateTime.Now, DateTime.Now);
    Images = ProductImages.Create();
    AverageRating = Rating.Empty;
  }

  public static Product Create(
    ProductName name,
    ProductDescription description,
    Price price,
    Stock stock,
    UserId sellerId,
    ProductImage thumbnail
  ) =>
    new(
      ProductId.CreateUnique(),
      name,
      description,
      price,
      stock,
      sellerId,
      thumbnail,
      DateTime.UtcNow,
      DateTime.UtcNow
    );

  public Product WithCategories(List<ProductCategory> categories)
  {
    _categories.AddRange(categories);
    return this;
  }

  public Product WithTags(List<ProductTag> tags)
  {
    _tags.AddRange(tags);
    return this;
  }

  public Product WithReviews(List<ProductReview> reviews)
  {
    _reviews.AddRange(reviews);
    return this;
  }

  public Product WithAverageRating(Rating averageRating)
  {
    AverageRating = averageRating;
    return this;
  }

  public Product WithPromotion(Promotion promotion)
  {
    Promotion = promotion;
    return this;
  }

  public Product WithImages(ProductImages images)
  {
    Images = images;
    return this;
  }

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
