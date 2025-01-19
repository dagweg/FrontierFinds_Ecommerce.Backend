namespace Ecommerce.Application.UseCases.Images.Common;

public class ImageResult
{
  public string Url { get; set; }
  public string FileType { get; set; }
  public string? FileName { get; set; }
  public string? FileSize { get; set; }

  public ImageResult(string url, string fileType, string? fileName = null, string? fileSize = null)
  {
    Url = url;
    FileType = fileType;
    FileName = fileName;
    FileSize = fileSize;
  }
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
  public ImageResult() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
}
