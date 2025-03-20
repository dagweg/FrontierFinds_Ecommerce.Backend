using Ecommerce.Domain.Common.ValueObjects;
using Ecommerce.Domain.ProductAggregate.Entities;
using Ecommerce.Domain.ProductAggregate.ValueObjects;
using Ecommerce.Domain.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure;

public static partial class Seeding
{
  public static class Product
  {
    public static readonly List<ProductId> productIds = new List<ProductId>()
    {
      ProductId.Convert(Guid.Parse("a1b2c3d4-e5f6-7890-1234-567890abcdef")),
      ProductId.Convert(Guid.Parse("b2c3d4e5-f678-9012-3456-7890abcdefa1")),
      ProductId.Convert(Guid.Parse("c3d4e5f6-7890-1234-5678-90abcdefa1b2")),
      ProductId.Convert(Guid.Parse("d4e5f678-9012-3456-7890-abcdefa1b2c3")),
      ProductId.Convert(Guid.Parse("e5f67890-1234-5678-90ab-cdefa1b2c3d4")),
      ProductId.Convert(Guid.Parse("f6789012-3456-7890-abcd-efa1b2c3d4e5")),
      ProductId.Convert(Guid.Parse("78901234-5678-90ab-cdef-a1b2c3d4e5f6")),
      ProductId.Convert(Guid.Parse("90123456-7890-abcd-efab-1b2c3d4e5f67")),
      ProductId.Convert(Guid.Parse("12345678-90ab-cdef-a1b2-c3d4e5f67890")),
      ProductId.Convert(Guid.Parse("34567890-abcd-efab-1b2c-3d4e5f678901")),
      ProductId.Convert(Guid.Parse("567890ab-cdef-a1b2-c3d4-e5f678901234")),
      ProductId.Convert(Guid.Parse("7890abcd-efab-1b2c-3d4e-5f6789012345")),
      ProductId.Convert(Guid.Parse("90abcdef-ab1b-2c3d-4e5f-678901234567")),
      ProductId.Convert(Guid.Parse("abcdefa1-b2c3-d4e5-f678-901234567890")),
      ProductId.Convert(Guid.Parse("bcdefa1b-c3d4-e5f6-7890-1234567890ab")),
      ProductId.Convert(Guid.Parse("cdefa1b2-d4e5-f678-9012-34567890abcd")),
    };

    public static List<Domain.ProductAggregate.Product> GetSeed()
    {
      return
      [
        Domain
          .ProductAggregate.Product.Create(
            name: ProductName.Create("Dell XPS 13"),
            slug: "dell-xps-13",
            description: ProductDescription.Create(
              "13-inch laptop with 11th Gen Intel Core i7, 16GB RAM, 512GB SSD"
            ),
            price: Price.CreateInBaseCurrency(120000),
            stock: Stock.Create(Quantity.Create(15)),
            sellerId: User.JohnDoeId, // Use the static field from Seeding.User
            thumbnail: ProductImage.Create("https://imgur.com/5OvyHwi.jpg", "Dell XPS 13 Thumbnail")
          )
          .WithProductId(productIds[0]) // Index 0
          .WithAverageRating(Rating.Create(3).Value)
          .WithPromotion(Domain.ProductAggregate.Entities.Promotion.CreateEmpty())
          .WithCategories([Seeding.Categories.Electronics_Laptops]),
        Domain
          .ProductAggregate.Product.Create(
            name: ProductName.Create("MacBook Pro 14"),
            slug: "macbook-pro-14",
            description: ProductDescription.Create(
              "14-inch laptop with M2 Pro chip, 32GB RAM, 1TB SSD"
            ),
            price: Price.CreateInBaseCurrency(199900),
            stock: Stock.Create(Quantity.Create(10)),
            sellerId: User.JohnDoeId,
            thumbnail: ProductImage.Create(
              "https://imgur.com/uwH8whc.jpg",
              "MacBook Pro 14 Thumbnail"
            )
          )
          .WithProductId(productIds[1])
          .WithAverageRating(Rating.Create(0).Value)
          .WithPromotion(Domain.ProductAggregate.Entities.Promotion.CreateEmpty())
          .WithCategories([Seeding.Categories.Electronics_Laptops]),
        Domain
          .ProductAggregate.Product.Create(
            name: ProductName.Create("Lenovo ThinkPad X1 Carbon"),
            slug: "lenovo-thinkpad-x1-carbon",
            description: ProductDescription.Create(
              "14-inch business laptop with Intel i5, 16GB RAM, 256GB SSD"
            ),
            price: Price.CreateInBaseCurrency(140000),
            stock: Stock.Create(Quantity.Create(20)),
            sellerId: User.JohnDoeId,
            thumbnail: ProductImage.Create(
              "https://imgur.com/uH5yghl.jpg",
              "ThinkPad X1 Carbon Thumbnail"
            )
          )
          .WithProductId(productIds[2])
          .WithAverageRating(Rating.Create(0).Value)
          .WithPromotion(Domain.ProductAggregate.Entities.Promotion.CreateEmpty())
          .WithCategories([Seeding.Categories.Electronics_Laptops]),
        Domain
          .ProductAggregate.Product.Create(
            name: ProductName.Create("HP Spectre x360"),
            slug: "hp-spectre-x360",
            description: ProductDescription.Create(
              "13.3-inch convertible laptop with Intel i7, 16GB RAM, 512GB SSD"
            ),
            price: Price.CreateInBaseCurrency(130000),
            stock: Stock.Create(Quantity.Create(12)),
            sellerId: User.JohnDoeId,
            thumbnail: ProductImage.Create(
              "https://imgur.com/hEZ06oD.jpg",
              "HP Spectre x360 Thumbnail"
            )
          )
          .WithProductId(productIds[3])
          .WithAverageRating(Rating.Create(0).Value)
          .WithPromotion(Domain.ProductAggregate.Entities.Promotion.CreateEmpty())
          .WithCategories([Seeding.Categories.Electronics_Laptops]),
        Domain
          .ProductAggregate.Product.Create(
            name: ProductName.Create("iPhone 14 Pro"),
            slug: "iphone-14-pro",
            description: ProductDescription.Create(
              "6.1-inch smartphone with A16 Bionic, 256GB storage"
            ),
            price: Price.CreateInBaseCurrency(99900),
            stock: Stock.Create(Quantity.Create(25)),
            sellerId: User.EmmaSmithId,
            thumbnail: ProductImage.Create(
              "https://imgur.com/J9GBcW2.jpg",
              "iPhone 14 Pro Thumbnail"
            )
          )
          .WithProductId(productIds[4])
          .WithAverageRating(Rating.Create(0).Value)
          .WithPromotion(Domain.ProductAggregate.Entities.Promotion.CreateEmpty())
          .WithCategories([Seeding.Categories.Electronics_MobilePhones]),
        Domain
          .ProductAggregate.Product.Create(
            name: ProductName.Create("Samsung Galaxy S23 Ultra"),
            slug: "samsung-galaxy-s23-ultra",
            description: ProductDescription.Create(
              "6.8-inch phone with Snapdragon 8 Gen 2, 512GB storage"
            ),
            price: Price.CreateInBaseCurrency(119900),
            stock: Stock.Create(Quantity.Create(18)),
            sellerId: User.EmmaSmithId,
            thumbnail: ProductImage.Create(
              "https://imgur.com/mQu38gW.jpg",
              "Galaxy S23 Ultra Thumbnail"
            )
          )
          .WithProductId(productIds[5])
          .WithAverageRating(Rating.Create(0).Value)
          .WithPromotion(Domain.ProductAggregate.Entities.Promotion.CreateEmpty())
          .WithCategories([Seeding.Categories.Electronics_MobilePhones]),
        Domain
          .ProductAggregate.Product.Create(
            name: ProductName.Create("Google Pixel 8"),
            slug: "google-pixel-8",
            description: ProductDescription.Create("6.2-inch phone with Tensor G3, 128GB storage"),
            price: Price.CreateInBaseCurrency(69900),
            stock: Stock.Create(Quantity.Create(30)),
            sellerId: User.EmmaSmithId,
            thumbnail: ProductImage.Create(
              "https://imgur.com/NBdCB35.jpg",
              "Google Pixel 8 Thumbnail"
            )
          )
          .WithProductId(productIds[6])
          .WithAverageRating(Rating.Create(0).Value)
          .WithPromotion(Domain.ProductAggregate.Entities.Promotion.CreateEmpty())
          .WithCategories([Seeding.Categories.Electronics_MobilePhones]),
        Domain
          .ProductAggregate.Product.Create(
            name: ProductName.Create("OnePlus 11"),
            slug: "oneplus-11",
            description: ProductDescription.Create(
              "6.7-inch phone with Snapdragon 8 Gen 2, 256GB storage"
            ),
            price: Price.CreateInBaseCurrency(79900),
            stock: Stock.Create(Quantity.Create(22)),
            sellerId: User.EmmaSmithId,
            thumbnail: ProductImage.Create("https://imgur.com/PDrm3uc.jpg", "OnePlus 11 Thumbnail")
          )
          .WithProductId(productIds[7])
          .WithAverageRating(Rating.Create(0).Value)
          .WithPromotion(Domain.ProductAggregate.Entities.Promotion.CreateEmpty())
          .WithCategories([Seeding.Categories.Electronics_MobilePhones]),
        Domain
          .ProductAggregate.Product.Create(
            name: ProductName.Create("Nike Air Max Hoodie"),
            slug: "nike-air-max-hoodie",
            description: ProductDescription.Create("Comfortable cotton hoodie in black, size M"),
            price: Price.CreateInBaseCurrency(6500),
            stock: Stock.Create(Quantity.Create(50)),
            sellerId: User.LiamJohnsonId,
            thumbnail: ProductImage.Create(
              "https://imgur.com/aEyc5Eb.jpg",
              "Nike Air Max Hoodie Thumbnail"
            )
          )
          .WithProductId(productIds[8])
          .WithAverageRating(Rating.Create(0).Value)
          .WithPromotion(Domain.ProductAggregate.Entities.Promotion.CreateEmpty())
          .WithCategories([Seeding.Categories.Fashion_MensClothing]),
        Domain
          .ProductAggregate.Product.Create(
            name: ProductName.Create("Adidas Ultraboost Sneakers"),
            slug: "adidas-ultraboost-sneakers",
            description: ProductDescription.Create(
              "White running shoes with Boost cushioning, size 10"
            ),
            price: Price.CreateInBaseCurrency(12000),
            stock: Stock.Create(Quantity.Create(40)),
            sellerId: User.LiamJohnsonId,
            thumbnail: ProductImage.Create(
              "https://imgur.com/919jfu3.jpg",
              "Adidas Ultraboost Thumbnail"
            )
          )
          .WithProductId(productIds[9])
          .WithAverageRating(Rating.Create(0).Value)
          .WithPromotion(Domain.ProductAggregate.Entities.Promotion.CreateEmpty())
          .WithCategories([Seeding.Categories.Fashion_Shoes]),
        Domain
          .ProductAggregate.Product.Create(
            name: ProductName.Create("Levi's 501 Jeans"),
            slug: "levis-501-jeans",
            description: ProductDescription.Create("Classic blue denim jeans, size 32"),
            price: Price.CreateInBaseCurrency(8000),
            stock: Stock.Create(Quantity.Create(35)),
            sellerId: User.LiamJohnsonId,
            thumbnail: ProductImage.Create(
              "https://imgur.com/6B4kfKt.jpg",
              "Levi's 501 Jeans Thumbnail"
            )
          )
          .WithProductId(productIds[10])
          .WithAverageRating(Rating.Create(0).Value)
          .WithPromotion(Domain.ProductAggregate.Entities.Promotion.CreateEmpty())
          .WithCategories([Seeding.Categories.Fashion_MensClothing]),
        Domain
          .ProductAggregate.Product.Create(
            name: ProductName.Create("Uniqlo Puffer Jacket"),
            slug: "uniqlo-puffer-jacket",
            description: ProductDescription.Create("Lightweight winter jacket in navy, size L"),
            price: Price.CreateInBaseCurrency(9000),
            stock: Stock.Create(Quantity.Create(25)),
            sellerId: User.LiamJohnsonId,
            thumbnail: ProductImage.Create(
              "https://imgur.com/RO2MMaV.jpg",
              "Uniqlo Puffer Jacket Thumbnail"
            )
          )
          .WithProductId(productIds[11])
          .WithAverageRating(Rating.Create(0).Value)
          .WithPromotion(Domain.ProductAggregate.Entities.Promotion.CreateEmpty())
          .WithCategories([Seeding.Categories.Fashion_MensClothing]),
        Domain
          .ProductAggregate.Product.Create(
            name: ProductName.Create("The Great Gatsby"),
            slug: "the-great-gatsby",
            description: ProductDescription.Create(
              "Classic novel by F. Scott Fitzgerald, paperback"
            ),
            price: Price.CreateInBaseCurrency(1200),
            stock: Stock.Create(Quantity.Create(100)),
            sellerId: User.OliviaBrownId,
            thumbnail: ProductImage.Create(
              "https://imgur.com/CTrJOrY.jpg",
              "The Great Gatsby Thumbnail"
            )
          )
          .WithProductId(productIds[12])
          .WithAverageRating(Rating.Create(0).Value)
          .WithPromotion(Domain.ProductAggregate.Entities.Promotion.CreateEmpty())
          .WithCategories([Seeding.Categories.BooksAndStationery_FictionBooks]),
        Domain
          .ProductAggregate.Product.Create(
            name: ProductName.Create("1984"),
            slug: "1984",
            description: ProductDescription.Create("Dystopian novel by George Orwell, hardcover"),
            price: Price.CreateInBaseCurrency(1500),
            stock: Stock.Create(Quantity.Create(80)),
            sellerId: User.OliviaBrownId,
            thumbnail: ProductImage.Create("https://imgur.com/huoJfPH.jpg", "1984 Thumbnail")
          )
          .WithProductId(productIds[13])
          .WithAverageRating(Rating.Create(0).Value)
          .WithPromotion(Domain.ProductAggregate.Entities.Promotion.CreateEmpty())
          .WithCategories([Seeding.Categories.BooksAndStationery_FictionBooks]),
        Domain
          .ProductAggregate.Product.Create(
            name: ProductName.Create("To Kill a Mockingbird"),
            slug: "to-kill-a-mockingbird",
            description: ProductDescription.Create(
              "Pulitzer Prize-winning novel by Harper Lee, paperback"
            ),
            price: Price.CreateInBaseCurrency(1000),
            stock: Stock.Create(Quantity.Create(90)),
            sellerId: User.OliviaBrownId,
            thumbnail: ProductImage.Create(
              "https://imgur.com/zME9cjl.jpg",
              "To Kill a Mockingbird Thumbnail"
            )
          )
          .WithProductId(productIds[14])
          .WithAverageRating(Rating.Create(0).Value)
          .WithPromotion(Domain.ProductAggregate.Entities.Promotion.CreateEmpty())
          .WithCategories([Seeding.Categories.BooksAndStationery_FictionBooks]),
        Domain
          .ProductAggregate.Product.Create(
            name: ProductName.Create("Dune"),
            slug: "dune",
            description: ProductDescription.Create("Sci-fi epic by Frank Herbert, hardcover"),
            price: Price.CreateInBaseCurrency(2000),
            stock: Stock.Create(Quantity.Create(70)),
            sellerId: User.OliviaBrownId,
            thumbnail: ProductImage.Create("https://imgur.com/RCWItyY.jpg", "Dune Thumbnail")
          )
          .WithProductId(productIds[15])
          .WithAverageRating(Rating.Create(0).Value)
          .WithPromotion(Domain.ProductAggregate.Entities.Promotion.CreateEmpty())
          .WithCategories([Seeding.Categories.BooksAndStationery_FictionBooks]),
      ];
    }
  }
}
