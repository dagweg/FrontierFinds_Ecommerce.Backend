namespace Ecommerce.Domain.ProductAggregate;

using Ecommerce.Domain.Common.Entities;
using Ecommerce.Domain.Common.Models;
using Ecommerce.Domain.Common.ValueObjects;
using Ecommerce.Domain.ProductAggregate.Entities;
using Ecommerce.Domain.ProductAggregate.ValueObjects;
using Ecommerce.Domain.UserAggregate.ValueObjects;

public class Product : AggregateRoot<ProductId>
{
  public ProductName Name { get; private set; } = ProductName.Empty;
  public ProductDescription Description { get; private set; } = ProductDescription.Empty;
  public Price Price { get; private set; } = Price.Empty;
  public Stock Stock { get; private set; } = Stock.Empty;
  public string Slug { get; private set; }
  public UserId SellerId { get; private set; } = UserId.Empty;

  private readonly List<Category> _categories = [];
  private readonly List<ProductTag> _tags = [];
  private readonly List<ProductReview> _reviews = [];

  public IReadOnlyList<Category> Categories => _categories.AsReadOnly();
  public IReadOnlyList<ProductTag> Tags => _tags.AsReadOnly();
  public IReadOnlyList<ProductReview> Reviews => _reviews.AsReadOnly();

  private int? _totalReviews;
  public int TotalReviews
  {
    get
    {
      if (!_totalReviews.HasValue)
      {
        _totalReviews = Reviews.Count;
      }
      return _totalReviews.Value;
    }
  }

  private decimal? _averageRatingValue; // nullable for caching
  public decimal AverageRatingValue
  {
    get
    {
      if (!_averageRatingValue.HasValue)
      {
        _averageRatingValue = Reviews.Any() ? Reviews.Select(x => x.Rating.Value).Average() : 0;
      }
      return _averageRatingValue.Value;
    }
  }
  public ProductImages Images { get; private set; }
  public Rating AverageRating { get; private set; }
  public Promotion Promotion { get; private set; }

  protected Product(
    ProductId productId,
    ProductName name,
    string slug,
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
    Slug = slug;
    Description = description;
    Price = price;
    Stock = stock;
    SellerId = sellerId;
    Promotion = Promotion.CreateEmpty();
    Images = ProductImages.Create(thumbnail);
    AverageRating = Rating.Create(0).Value;
  }

  public static Product Create(
    ProductName name,
    string slug,
    ProductDescription description,
    Price price,
    Stock stock,
    UserId sellerId,
    ProductImage thumbnail
  ) =>
    new(
      ProductId.CreateUnique(),
      name,
      slug,
      description,
      price,
      stock,
      sellerId,
      thumbnail,
      DateTime.UtcNow,
      DateTime.UtcNow
    );

  public Product WithCategories(List<Category> categories)
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

  public Product WithProductId(ProductId productId)
  {
    Id = productId;
    return this;
  }

  public Product WithSlug(string slug)
  {
    Slug = slug;
    return this;
  }

  public void UpdateName(ProductName name) => Name = name;

  public void UpdateDescription(ProductDescription description) => Description = description;

  public void UpdatePrice(Price price) => Price = price;

  public void UpdateStock(Stock stock) => Stock = stock;

  public void AddReview(ProductReview review)
  {
    _reviews.Add(review);

    // invalidate the cache
    _averageRatingValue = null;
    _totalReviews = null;
  }

  public void AddRating(Rating rating)
  {
    AverageRating = Rating
      .Create(Math.Clamp((AverageRating.Value + rating.Value) / 2m, 1, 5))
      .Value;
  }

  public override IEnumerable<object> GetEqualityComponents()
  {
    yield return Id;
  }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
  protected Product()
    : base(ProductId.Empty) { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
}
