using FluentResults;

namespace Ecommerce.Application.Common.Interfaces.Processors;

public interface IImageProcessor
{
  Result<string> GetImageFormat(Stream imageStream);

  /// <summary>
  /// Get the size of the image in bytes
  /// </summary>
  /// <param name="imageStream"></param>
  /// <returns>
  /// The size of the image in bytes
  /// </returns>
  Result<long> GetImageSize(Stream imageStream);

  /// <summary>
  /// Compresses the image with the specified quality to the desired format.
  /// </summary>
  /// <param name="imageStream"></param>
  /// <param name="imageFormat"></param>
  /// <param name="quality"></param>
  /// <returns></returns>
  Task<Result<Stream>> CompressImageAsync(
    Stream imageStream,
    string imageFormat = "webp",
    int quality = 75
  );

  /// <summary>
  /// Watermark the image
  /// </summary>
  /// <param name="imageStream"></param>
  /// <param name="watermarkText"></param>
  /// <returns> A stream of image data</returns>
  Task<Result<Stream>> WatermarkImageAsync(Stream imageStream, string watermarkText);
}
