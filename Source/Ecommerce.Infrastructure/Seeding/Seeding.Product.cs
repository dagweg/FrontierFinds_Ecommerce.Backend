using Ecommerce.Domain.ProductAggregate.ValueObjects;
using Ecommerce.Domain.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Infrastructure;

public static partial class Seeding
{
  public static class Product
  {
    private static readonly List<ProductId> productIds = Enumerable
      .Range(0, 16)
      .Select(_ => ProductId.CreateUnique())
      .ToList();

    public static void Seed(ModelBuilder builder)
    {
      builder
        .Entity<Domain.ProductAggregate.Product>()
        .HasData(
          new
          {
            Id = productIds[0],
            Name = ProductName.Create("Dell XPS 13"),
            Description = ProductDescription.Create(
              "13-inch laptop with 11th Gen Intel Core i7, 16GB RAM, 512GB SSD"
            ),
            Price_ValueInCents = 120000,
            Stock_Quantity_Value = 15,
            SellerId = User.JohnDoeId, // Use the static field from Seeding.User
            AverageRating_Value = 0,
          },
          new
          {
            Id = productIds[1],
            Name = ProductName.Create("MacBook Pro 14"),
            Description = ProductDescription.Create(
              "14-inch laptop with M2 Pro chip, 32GB RAM, 1TB SSD"
            ),
            Price_ValueInCents = 199900,
            Stock_Quantity_Value = 10,
            SellerId = User.JohnDoeId, // Use the static field
            AverageRating_Value = 0,
          },
          new
          {
            Id = productIds[2],
            Name = ProductName.Create("Lenovo ThinkPad X1 Carbon"),
            Description = ProductDescription.Create(
              "14-inch business laptop with Intel i5, 16GB RAM, 256GB SSD"
            ),
            Price_ValueInCents = 140000,
            Stock_Quantity_Value = 20,
            SellerId = User.JohnDoeId, // Use the static field
            AverageRating_Value = 0,
          },
          new
          {
            Id = productIds[3],
            Name = ProductName.Create("HP Spectre x360"),
            Description = ProductDescription.Create(
              "13.3-inch convertible laptop with Intel i7, 16GB RAM, 512GB SSD"
            ),
            Price_ValueInCents = 130000,
            Stock_Quantity_Value = 12,
            SellerId = User.JohnDoeId, // Use the static field
            AverageRating_Value = 0,
          },
          new
          {
            Id = productIds[4],
            Name = ProductName.Create("iPhone 14 Pro"),
            Description = ProductDescription.Create(
              "6.1-inch smartphone with A16 Bionic, 256GB storage"
            ),
            Price_ValueInCents = 99900,
            Stock_Quantity_Value = 25,
            SellerId = User.EmmaSmithId, // Use the static field
            AverageRating_Value = 0,
          },
          new
          {
            Id = productIds[5],
            Name = ProductName.Create("Samsung Galaxy S23 Ultra"),
            Description = ProductDescription.Create(
              "6.8-inch phone with Snapdragon 8 Gen 2, 512GB storage"
            ),
            Price_ValueInCents = 119900,
            Stock_Quantity_Value = 18,
            SellerId = User.EmmaSmithId, // Use the static field
            AverageRating_Value = 0,
          },
          new
          {
            Id = productIds[6],
            Name = ProductName.Create("Google Pixel 8"),
            Description = ProductDescription.Create("6.2-inch phone with Tensor G3, 128GB storage"),
            Price_ValueInCents = 69900,
            Stock_Quantity_Value = 30,
            SellerId = User.EmmaSmithId, // Use the static field
            AverageRating_Value = 0,
          },
          new
          {
            Id = productIds[7],
            Name = ProductName.Create("OnePlus 11"),
            Description = ProductDescription.Create(
              "6.7-inch phone with Snapdragon 8 Gen 2, 256GB storage"
            ),
            Price_ValueInCents = 79900,
            Stock_Quantity_Value = 22,
            SellerId = User.EmmaSmithId, // Use the static field
            AverageRating_Value = 0,
          },
          new
          {
            Id = productIds[8],
            Name = ProductName.Create("Nike Air Max Hoodie"),
            Description = ProductDescription.Create("Comfortable cotton hoodie in black, size M"),
            Price_ValueInCents = 6500,
            Stock_Quantity_Value = 50,
            SellerId = User.LiamJohnsonId, // Use the static field
            AverageRating_Value = 0,
          },
          new
          {
            Id = productIds[9],
            Name = ProductName.Create("Adidas Ultraboost Sneakers"),
            Description = ProductDescription.Create(
              "White running shoes with Boost cushioning, size 10"
            ),
            Price_ValueInCents = 12000,
            Stock_Quantity_Value = 40,
            SellerId = User.LiamJohnsonId, // Use the static field
            AverageRating_Value = 0,
          },
          new
          {
            Id = productIds[10],
            Name = ProductName.Create("Levi's 501 Jeans"),
            Description = ProductDescription.Create("Classic blue denim jeans, size 32"),
            Price_ValueInCents = 8000,
            Stock_Quantity_Value = 35,
            SellerId = User.LiamJohnsonId, // Use the static field
            AverageRating_Value = 0,
          },
          new
          {
            Id = productIds[11],
            Name = ProductName.Create("Uniqlo Puffer Jacket"),
            Description = ProductDescription.Create("Lightweight winter jacket in navy, size L"),
            Price_ValueInCents = 9000,
            Stock_Quantity_Value = 25,
            SellerId = User.LiamJohnsonId, // Use the static field
            AverageRating_Value = 0,
          },
          new
          {
            Id = productIds[12],
            Name = ProductName.Create("The Great Gatsby"),
            Description = ProductDescription.Create(
              "Classic novel by F. Scott Fitzgerald, paperback"
            ),
            Price_ValueInCents = 1200,
            Stock_Quantity_Value = 100,
            SellerId = User.OliviaBrownId, // Use the static field
            AverageRating_Value = 0,
          },
          new
          {
            Id = productIds[13],
            Name = ProductName.Create("1984"),
            Description = ProductDescription.Create("Dystopian novel by George Orwell, hardcover"),
            Price_ValueInCents = 1500,
            Stock_Quantity_Value = 80,
            SellerId = User.OliviaBrownId, // Use the static field
            AverageRating_Value = 0,
          },
          new
          {
            Id = productIds[14],
            Name = ProductName.Create("To Kill a Mockingbird"),
            Description = ProductDescription.Create(
              "Pulitzer Prize-winning novel by Harper Lee, paperback"
            ),
            Price_ValueInCents = 1000,
            Stock_Quantity_Value = 90,
            SellerId = User.OliviaBrownId, // Use the static field
            AverageRating_Value = 0,
          },
          new
          {
            Id = productIds[15],
            Name = ProductName.Create("Dune"),
            Description = ProductDescription.Create("Sci-fi epic by Frank Herbert, hardcover"),
            Price_ValueInCents = 2000,
            Stock_Quantity_Value = 70,
            SellerId = User.OliviaBrownId, // Use the static field
            AverageRating_Value = 0,
          }
        );
    }

    public static class ProductImages
    {
      public static void Seed(ModelBuilder builder)
      {
        builder
          .Entity<Domain.ProductAggregate.Entities.ProductImages>()
          .HasData(
            new
            {
              Id = Guid.NewGuid(),
              ProductId = productIds[0],
              Thumbnail_Url = "https://imgur.com/5OvyHwi.jpg",
              Thumbnail_ObjectIdentifier = "Dell XPS 13 Thumbnail",
            },
            new
            {
              Id = Guid.NewGuid(),
              ProductId = productIds[1],
              Thumbnail_Url = "https://imgur.com/uwH8whc.jpg",
              Thumbnail_ObjectIdentifier = "MacBook Pro 14 Thumbnail",
            },
            new
            {
              Id = Guid.NewGuid(),
              ProductId = productIds[2],
              Thumbnail_Url = "https://imgur.com/uH5yghl.jpg",
              Thumbnail_ObjectIdentifier = "ThinkPad X1 Carbon Thumbnail",
            },
            new
            {
              Id = Guid.NewGuid(),
              ProductId = productIds[3],
              Thumbnail_Url = "https://imgur.com/hEZ06oD.jpg",
              Thumbnail_ObjectIdentifier = "HP Spectre x360 Thumbnail",
            },
            new
            {
              Id = Guid.NewGuid(),
              ProductId = productIds[4],
              Thumbnail_Url = "https://imgur.com/J9GBcW2.jpg",
              Thumbnail_ObjectIdentifier = "iPhone 14 Pro Thumbnail",
            },
            new
            {
              Id = Guid.NewGuid(),
              ProductId = productIds[5],
              Thumbnail_Url = "https://imgur.com/mQu38gW.jpg",
              Thumbnail_ObjectIdentifier = "Galaxy S23 Ultra Thumbnail",
            },
            new
            {
              Id = Guid.NewGuid(),
              ProductId = productIds[6],
              Thumbnail_Url = "https://imgur.com/NBdCB35.jpg",
              Thumbnail_ObjectIdentifier = "Google Pixel 8 Thumbnail",
            },
            new
            {
              Id = Guid.NewGuid(),
              ProductId = productIds[7],
              Thumbnail_Url = "https://imgur.com/PDrm3uc.jpg",
              Thumbnail_ObjectIdentifier = "OnePlus 11 Thumbnail",
            },
            new
            {
              Id = Guid.NewGuid(),
              ProductId = productIds[8],
              Thumbnail_Url = "https://imgur.com/aEyc5Eb.jpg",
              Thumbnail_ObjectIdentifier = "Nike Air Max Hoodie Thumbnail",
            },
            new
            {
              Id = Guid.NewGuid(),
              ProductId = productIds[9],
              Thumbnail_Url = "https://imgur.com/919jfu3.jpg",
              Thumbnail_ObjectIdentifier = "Adidas Ultraboost Thumbnail",
            },
            new
            {
              Id = Guid.NewGuid(),
              ProductId = productIds[10],
              Thumbnail_Url = "https://imgur.com/6B4kfKt.jpg",
              Thumbnail_ObjectIdentifier = "Levi's 501 Jeans Thumbnail",
            },
            new
            {
              Id = Guid.NewGuid(),
              ProductId = productIds[11],
              Thumbnail_Url = "https://imgur.com/RO2MMaV.jpg",
              Thumbnail_ObjectIdentifier = "Uniqlo Puffer Jacket Thumbnail",
            },
            new
            {
              Id = Guid.NewGuid(),
              ProductId = productIds[12],
              Thumbnail_Url = "https://imgur.com/CTrJOrY.jpg",
              Thumbnail_ObjectIdentifier = "The Great Gatsby Thumbnail",
            },
            new
            {
              Id = Guid.NewGuid(),
              ProductId = productIds[13],
              Thumbnail_Url = "https://imgur.com/huoJfPH.jpg",
              Thumbnail_ObjectIdentifier = "1984 Thumbnail",
            },
            new
            {
              Id = Guid.NewGuid(),
              ProductId = productIds[14],
              Thumbnail_Url = "https://imgur.com/zME9cjl.jpg",
              Thumbnail_ObjectIdentifier = "To Kill a Mockingbird Thumbnail",
            },
            new
            {
              Id = Guid.NewGuid(),
              ProductId = productIds[15],
              Thumbnail_Url = "https://imgur.com/RCWItyY.jpg",
              Thumbnail_ObjectIdentifier = "Dune Thumbnail",
            }
          );
      }
    }

    public static class Promotion
    {
      public static void Seed(ModelBuilder builder)
      {
        builder
          .Entity<Domain.ProductAggregate.Entities.Promotion>()
          .HasData(
            new
            {
              Id = Guid.NewGuid(),
              ProductId = productIds[0],
              DiscountPercentage = 0,
              StartDate = new DateTime(2024, 01, 01),
              EndDate = new DateTime(2025, 01, 01),
            },
            new
            {
              Id = Guid.NewGuid(),
              ProductId = productIds[1],
              DiscountPercentage = 0,
              StartDate = new DateTime(2024, 01, 01),
              EndDate = new DateTime(2025, 01, 01),
            },
            new
            {
              Id = Guid.NewGuid(),
              ProductId = productIds[2],
              DiscountPercentage = 0,
              StartDate = new DateTime(2024, 01, 01),
              EndDate = new DateTime(2025, 01, 01),
            },
            new
            {
              Id = Guid.NewGuid(),
              ProductId = productIds[3],
              DiscountPercentage = 0,
              StartDate = new DateTime(2024, 01, 01),
              EndDate = new DateTime(2025, 01, 01),
            },
            new
            {
              Id = Guid.NewGuid(),
              ProductId = productIds[4],
              DiscountPercentage = 0,
              StartDate = new DateTime(2024, 01, 01),
              EndDate = new DateTime(2025, 01, 01),
            },
            new
            {
              Id = Guid.NewGuid(),
              ProductId = productIds[5],
              DiscountPercentage = 0,
              StartDate = new DateTime(2024, 01, 01),
              EndDate = new DateTime(2025, 01, 01),
            },
            new
            {
              Id = Guid.NewGuid(),
              ProductId = productIds[6],
              DiscountPercentage = 0,
              StartDate = new DateTime(2024, 01, 01),
              EndDate = new DateTime(2025, 01, 01),
            },
            new
            {
              Id = Guid.NewGuid(),
              ProductId = productIds[7],
              DiscountPercentage = 0,
              StartDate = new DateTime(2024, 01, 01),
              EndDate = new DateTime(2025, 01, 01),
            },
            new
            {
              Id = Guid.NewGuid(),
              ProductId = productIds[8],
              DiscountPercentage = 0,
              StartDate = new DateTime(2024, 01, 01),
              EndDate = new DateTime(2025, 01, 01),
            },
            new
            {
              Id = Guid.NewGuid(),
              ProductId = productIds[9],
              DiscountPercentage = 0,
              StartDate = new DateTime(2024, 01, 01),
              EndDate = new DateTime(2025, 01, 01),
            },
            new
            {
              Id = Guid.NewGuid(),
              ProductId = productIds[10],
              DiscountPercentage = 0,
              StartDate = new DateTime(2024, 01, 01),
              EndDate = new DateTime(2025, 01, 01),
            },
            new
            {
              Id = Guid.NewGuid(),
              ProductId = productIds[11],
              DiscountPercentage = 0,
              StartDate = new DateTime(2024, 01, 01),
              EndDate = new DateTime(2025, 01, 01),
            },
            new
            {
              Id = Guid.NewGuid(),
              ProductId = productIds[12],
              DiscountPercentage = 0,
              StartDate = new DateTime(2024, 01, 01),
              EndDate = new DateTime(2025, 01, 01),
            },
            new
            {
              Id = Guid.NewGuid(),
              ProductId = productIds[13],
              DiscountPercentage = 0,
              StartDate = new DateTime(2024, 01, 01),
              EndDate = new DateTime(2025, 01, 01),
            },
            new
            {
              Id = Guid.NewGuid(),
              ProductId = productIds[14],
              DiscountPercentage = 0,
              StartDate = new DateTime(2024, 01, 01),
              EndDate = new DateTime(2025, 01, 01),
            },
            new
            {
              Id = Guid.NewGuid(),
              ProductId = productIds[15],
              DiscountPercentage = 0,
              StartDate = new DateTime(2024, 01, 01),
              EndDate = new DateTime(2025, 01, 01),
            }
          );
      }
    }
  }
}
