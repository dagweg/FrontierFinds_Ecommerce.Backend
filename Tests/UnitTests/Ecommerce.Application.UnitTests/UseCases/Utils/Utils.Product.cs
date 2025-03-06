using System.Text;
using Ecommerce.Application.UseCases.Images.CreateImage;
using Ecommerce.Application.UseCases.Products.Common;
using Ecommerce.Application.UseCases.Products.CreateUser.Commands;
using Ecommerce.Domain.ProductAggregate.Entities;
using Ecommerce.Domain.UserAggregate.ValueObjects;

namespace Ecommerce.UnitTests.Ecommerce.Application.UnitTests.UseCases;

public partial class Utils
{
  public record Product
  {
    public static readonly string Id = Guid.NewGuid().ToString();
    public const string ProductName = "Product Name";
    public const string ProductDescription = "Product Description";
    public const int StockQuantity = 10;
    public const decimal PriceValue = 2000;
    public const string PriceCurrency = "ETB";

    public static string ProductNameFromIndex(int i) => $"{ProductName} {i}";

    public static string ProductDescriptionFromIndex(int i) => $"{ProductDescription} {i}";

    public static CreateProductCommand CreateCommand(int i = 0)
    {
      return new CreateProductCommand(
        ProductName: Utils.Product.ProductNameFromIndex(i),
        ProductDescription: Utils.Product.ProductDescriptionFromIndex(i),
        StockQuantity: Utils.Product.StockQuantity,
        PriceValue: Utils.Product.PriceValue,
        PriceCurrency: Utils.Product.PriceCurrency,
        Thumbnail: Utils.Product.Thumbnail.CreateCommand(i),
        LeftImage: Utils.Product.LeftImage.CreateCommand(i),
        RightImage: Utils.Product.RightImage.CreateCommand(i),
        FrontImage: Utils.Product.FrontImage.CreateCommand(i),
        BackImage: Utils.Product.BackImage.CreateCommand(i)
      );
    }

    // create a static method to create a Product
    public static Domain.ProductAggregate.Product CreateProduct(int i = 1)
    {
      return Domain
        .ProductAggregate.Product.Create(
          name: Domain.ProductAggregate.ValueObjects.ProductName.Create(
            Utils.Product.ProductNameFromIndex(i)
          ),
          description: Domain.ProductAggregate.ValueObjects.ProductDescription.Create(
            Utils.Product.ProductDescriptionFromIndex(i)
          ),
          stock: CreateStock(Utils.Product.StockQuantity),
          price: Domain.Common.ValueObjects.Price.CreateInBaseCurrency(Utils.Product.PriceValue),
          thumbnail: CreateProductImage(
            Utils.Product.Thumbnail.Url,
            Utils.Product.Thumbnail.ObjectIdentifier
          ),
          sellerId: UserId.CreateUnique()
        )
        .WithImages(
          ProductImages
            .Create(Utils.Product.Thumbnail.CreateProductImage())
            .WithLeftImage(Utils.Product.LeftImage.CreateProductImage())
            .WithRightImage(Utils.Product.RightImage.CreateProductImage())
            .WithFrontImage(Utils.Product.FrontImage.CreateProductImage())
            .WithBackImage(Utils.Product.BackImage.CreateProductImage())
        );
    }

    public static ProductImage CreateProductImage(string url, string oid)
    {
      return ProductImage.Create(url, oid);
    }

    public static Domain.ProductAggregate.ValueObjects.Stock CreateStock(int quantity)
    {
      return Domain.ProductAggregate.ValueObjects.Stock.Create(
        Domain.ProductAggregate.ValueObjects.Quantity.Create(quantity)
      );
    }

    public static ProductResult CreateProductResult(Domain.ProductAggregate.Product product)
    {
      return new ProductResult
      {
        ProductId = product.Id.Value.ToString(),
        ProductName = product.Name.Value,
        ProductDescription = product.Description.Value,
        StockQuantity = product.Stock.Quantity,
        PriceValue = product.Price.Value,
        PriceCurrency = Domain.Common.ValueObjects.Price.BASE_CURRENCY.ToString(),
        Images = new ProductImagesResult
        {
          Thumbnail = Utils.Product.Thumbnail.CreateProductImageResult(),
          LeftImage = Utils.Product.LeftImage.CreateProductImageResult(),
          RightImage = Utils.Product.RightImage.CreateProductImageResult(),
          FrontImage = Utils.Product.FrontImage.CreateProductImageResult(),
          BackImage = Utils.Product.BackImage.CreateProductImageResult(),
          TopImage = Utils.Product.TopImage.CreateProductImageResult(),
          BottomImage = Utils.Product.BottomImage.CreateProductImageResult(),
        },
        Tags = Utils.Product.Tag.CreateTagResults(3),
      };
    }

    public record Thumbnail
    {
      public const string FileName = "Thumbnail";
      public const string Url = "ThumbnailUrl";
      public const string ObjectIdentifier = "ThumbnailOid";
      public const string ImageData = "ThumbnailData";
      public static readonly Stream ImageStream = new MemoryStream(
        Encoding.UTF8.GetBytes(ImageData)
      );

      public static CreateImageCommand CreateCommand(int i = 0)
      {
        return new CreateImageCommand { FileName = $"{FileName} {i}", ImageStream = ImageStream };
      }

      public static ProductImage CreateProductImage()
      {
        return ProductImage.Create(Url, ObjectIdentifier);
      }

      public static ProductImageResult CreateProductImageResult()
      {
        return new ProductImageResult { Url = Url };
      }
    }

    public record LeftImage
    {
      public const string FileName = "LeftImage";
      public const string Url = "LeftImageUrl";
      public const string ObjectIdentifier = "LeftImageOid";
      public const string ImageData = "LeftImageData";
      public static readonly Stream ImageStream = new MemoryStream(
        Encoding.UTF8.GetBytes(ImageData)
      );

      public static CreateImageCommand CreateCommand(int i = 0)
      {
        return new CreateImageCommand { FileName = $"{FileName} {i}", ImageStream = ImageStream };
      }

      public static ProductImage CreateProductImage()
      {
        return ProductImage.Create(Url, ObjectIdentifier);
      }

      public static ProductImageResult CreateProductImageResult()
      {
        return new ProductImageResult { Url = Url };
      }
    }

    public record RightImage
    {
      public const string FileName = "RightImage";
      public const string Url = "RightImageUrl";
      public const string ObjectIdentifier = "RightImageOid";
      public const string ImageData = "RightImageData";
      public static readonly Stream ImageStream = new MemoryStream(
        Encoding.UTF8.GetBytes(ImageData)
      );

      public static CreateImageCommand CreateCommand(int i = 0)
      {
        return new CreateImageCommand { FileName = $"{FileName} {i}", ImageStream = ImageStream };
      }

      public static ProductImage CreateProductImage()
      {
        return ProductImage.Create(Url, ObjectIdentifier);
      }

      public static ProductImageResult CreateProductImageResult()
      {
        return new ProductImageResult { Url = Url };
      }
    }

    public record BackImage
    {
      public const string FileName = "BackImage";
      public const string Url = "BackImageUrl";
      public const string ObjectIdentifier = "BackImageOid";
      public const string ImageData = "BackImageData";
      public static readonly Stream ImageStream = new MemoryStream(
        Encoding.UTF8.GetBytes(ImageData)
      );

      public static CreateImageCommand CreateCommand(int i = 0)
      {
        return new CreateImageCommand { FileName = $"{FileName} {i}", ImageStream = ImageStream };
      }

      public static ProductImage CreateProductImage()
      {
        return ProductImage.Create(Url, ObjectIdentifier);
      }

      public static ProductImageResult CreateProductImageResult()
      {
        return new ProductImageResult { Url = Url };
      }
    }

    public record FrontImage
    {
      public const string FileName = "FrontImage";
      public const string Url = "FrontImageUrl";
      public const string ObjectIdentifier = "FrontImageOid";
      public const string ImageData = "FrontImageData";
      public static readonly Stream ImageStream = new MemoryStream(
        Encoding.UTF8.GetBytes(ImageData)
      );

      public static CreateImageCommand CreateCommand(int i = 0)
      {
        return new CreateImageCommand { FileName = $"{FileName} {i}", ImageStream = ImageStream };
      }

      public static ProductImage CreateProductImage()
      {
        return ProductImage.Create(Url, ObjectIdentifier);
      }

      public static ProductImageResult CreateProductImageResult()
      {
        return new ProductImageResult { Url = Url };
      }
    }

    public record TopImage
    {
      public const string FileName = "TopImageImage";
      public const string Url = "TopImageUrl";
      public const string ObjectIdentifier = "TopImageOid";
      public const string ImageData = "TopImageData";
      public static readonly Stream ImageStream = new MemoryStream(
        Encoding.UTF8.GetBytes(ImageData)
      );

      public static CreateImageCommand CreateCommand(int i = 0)
      {
        return new CreateImageCommand { FileName = $"{FileName} {i}", ImageStream = ImageStream };
      }

      public static ProductImage CreateProductImage()
      {
        return ProductImage.Create(Url, ObjectIdentifier);
      }

      public static ProductImageResult CreateProductImageResult()
      {
        return new ProductImageResult { Url = Url };
      }
    }

    public record BottomImage
    {
      public const string FileName = "BottomImageImage";
      public const string Url = "BottomImageUrl";
      public const string ObjectIdentifier = "BottomImageOid";
      public const string ImageData = "BottomImageData";
      public static readonly Stream ImageStream = new MemoryStream(
        Encoding.UTF8.GetBytes(ImageData)
      );

      public static CreateImageCommand CreateCommand(int i = 0)
      {
        return new CreateImageCommand { FileName = $"{FileName} {i}", ImageStream = ImageStream };
      }

      public static ProductImage CreateProductImage()
      {
        return ProductImage.Create(Url, ObjectIdentifier);
      }

      public static ProductImageResult CreateProductImageResult()
      {
        return new ProductImageResult { Url = Url };
      }
    };

    public record Tag
    {
      public static List<Domain.ProductAggregate.Entities.ProductTag> CreateTags(int i = 1)
      {
        return new List<Domain.ProductAggregate.Entities.ProductTag>
        {
          Domain.ProductAggregate.Entities.ProductTag.Create(Guid.NewGuid(), $"Tag {i}"),
        };
      }

      public static List<TagResult> CreateTagResults(int i = 1)
      {
        return CreateTags(i).Select(t => new TagResult { Id = t.Id, Name = t.Name }).ToList();
      }
    }
  }
}
