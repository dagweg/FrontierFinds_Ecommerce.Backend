using System.Text;

namespace Ecommerce.Tests.Shared;

public partial class Utils
{
  public record Image
  {
    public const string ImageData = "ImageData";
    public static readonly Stream ImageStream = new MemoryStream(Encoding.UTF8.GetBytes(ImageData));
    public static long ImageSize => ImageStream.Length;

    public static readonly string FileName = nameof(FileName);
    public static readonly string LeftImageFileName = nameof(LeftImageFileName);
    public static readonly string RightImageFileName = nameof(RightImageFileName);
    public static readonly string TopImageFileName = nameof(TopImageFileName);
    public static readonly string BackImageFileName = nameof(BackImageFileName);
    public static readonly string FrontImageFileName = nameof(FrontImageFileName);
    public static readonly string BottomImageFileName = nameof(BottomImageFileName);

    public static string ImageDataFromIndex(int i) => $"{ImageData} {i}";

    public static Stream ImageStreamFromIndex(int i) =>
      new MemoryStream(Encoding.UTF8.GetBytes(ImageDataFromIndex(i)));

    public static long ImageSizeFromIndex(int i) => ImageStreamFromIndex(i).Length;

    public static string FileNameFromIndex(int i) => $"{FileName} {i}";

    public static string LeftImageFileNameFromIndex(int i) => $"{LeftImageFileName} {i}";

    public static string RightImageFileNameFromIndex(int i) => $"{RightImageFileName} {i}";

    public static string TopImageFileNameFromIndex(int i) => $"{TopImageFileName} {i}";

    public static string BackImageFileNameFromIndex(int i) => $"{BackImageFileName} {i}";

    public static string FrontImageFileNameFromIndex(int i) => $"{FrontImageFileName} {i}";

    public static string BottomImageFileNameFromIndex(int i) => $"{BottomImageFileName} {i}";
  }
}
