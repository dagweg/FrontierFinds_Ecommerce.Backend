using Ecommerce.Domain.Common.Models;
using Ecommerce.Domain.Common.ValueObjects;
using Ecommerce.Domain.Product.Entities;
using Ecommerce.Domain.Product.ValueObjects;

namespace Ecommerce.Domain.Product;

public sealed class Product : AggregateRoot<ProductId>, ITimeStamped
{
    private readonly List<ProductCategory> _categories;


    private Product(
        ProductId productId,
        string name,
        string description,
        Price price,
        Stock stock,
        UserId sellerId,
        DateTime createdAt,
        DateTime updatedAt, List<ProductCategory> categories) : base(productId)
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

    public string Name { get; }
    public string Description { get; }
    public Price Price { get; }
    public Stock Stock { get; }
    public UserId SellerId { get; }

    public IReadOnlyList<ProductCategory> Categories => _categories.AsReadOnly();
    public DateTime CreatedAt { get; }
    public DateTime UpdatedAt { get; }

    public static Product Create(string name, string description, Price price, Stock stock)
    {
        return new Product(
            ProductId.CreateUnique(),
            name,
            description,
            price,
            stock,
            UserId.CreateUnique(),
            DateTime.UtcNow,
            DateTime.UtcNow,
            []
        );
    }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Id;
        yield return Name;
        yield return Description;
        yield return Price;
        yield return Stock;
        yield return SellerId;
        yield return CreatedAt;
        yield return UpdatedAt;
    }
}