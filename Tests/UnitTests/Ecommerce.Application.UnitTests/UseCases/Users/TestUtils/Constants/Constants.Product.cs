using System.Text;
using Ecommerce.Application.UseCases.Images.CreateImage;
using Ecommerce.Domain.UserAggregate.ValueObjects;

namespace Ecommerce.Application.UnitTests.UseCases.Users.TestUtils;

public partial class Constants
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

        public record Thumbnail
        {
            public const string FileName = "Thumbnail";
            public const string ImageData = "ThumbnailData";
            public static readonly Stream ImageStream = new MemoryStream(
              Encoding.UTF8.GetBytes(ImageData)
            );

            public static CreateImageCommand CreateCommand(int i = 0)
            {
                return new CreateImageCommand { FileName = $"{FileName} {i}", ImageStream = ImageStream };
            }
        }

        public record LeftImage
        {
            public const string FileName = "LeftImage";
            public const string ImageData = "LeftImageData";
            public static readonly Stream ImageStream = new MemoryStream(
              Encoding.UTF8.GetBytes(ImageData)
            );

            public static CreateImageCommand CreateCommand(int i = 0)
            {
                return new CreateImageCommand { FileName = $"{FileName} {i}", ImageStream = ImageStream };
            }
        }

        public record RightImage
        {
            public const string FileName = "RightImage";
            public const string ImageData = "RightImageData";
            public static readonly Stream ImageStream = new MemoryStream(
              Encoding.UTF8.GetBytes(ImageData)
            );

            public static CreateImageCommand CreateCommand(int i = 0)
            {
                return new CreateImageCommand { FileName = $"{FileName} {i}", ImageStream = ImageStream };
            }
        }

        public record BackImage
        {
            public const string FileName = "BackImage";
            public const string ImageData = "BackImageData";
            public static readonly Stream ImageStream = new MemoryStream(
              Encoding.UTF8.GetBytes(ImageData)
            );

            public static CreateImageCommand CreateCommand(int i = 0)
            {
                return new CreateImageCommand { FileName = $"{FileName} {i}", ImageStream = ImageStream };
            }
        }

        public record FrontImage
        {
            public const string FileName = "FrontImage";
            public const string ImageData = "FrontImageData";
            public static readonly Stream ImageStream = new MemoryStream(
              Encoding.UTF8.GetBytes(ImageData)
            );

            public static CreateImageCommand CreateCommand(int i = 0)
            {
                return new CreateImageCommand { FileName = $"{FileName} {i}", ImageStream = ImageStream };
            }
        }

        public record TopImage
        {
            public const string FileName = "TopImageImage";
            public const string ImageData = "TopImageData";
            public static readonly Stream ImageStream = new MemoryStream(
              Encoding.UTF8.GetBytes(ImageData)
            );

            public static CreateImageCommand CreateCommand(int i = 0)
            {
                return new CreateImageCommand { FileName = $"{FileName} {i}", ImageStream = ImageStream };
            }
        }

        public record BottomImage
        {
            public const string FileName = "BottomImageImage";
            public const string ImageData = "BottomImageData";
            public static readonly Stream ImageStream = new MemoryStream(
              Encoding.UTF8.GetBytes(ImageData)
            );

            public static CreateImageCommand CreateCommand(int i = 0)
            {
                return new CreateImageCommand { FileName = $"{FileName} {i}", ImageStream = ImageStream };
            }
        };

        public static readonly List<string> Tags = ["TAG1", "TAG2", "TAG3"];
    }
}
