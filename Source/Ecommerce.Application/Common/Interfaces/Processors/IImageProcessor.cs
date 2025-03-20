using System.IO;
using System.Threading.Tasks;
using FluentResults;

namespace Ecommerce.Application.Common.Interfaces.Processors;

public interface IImageProcessor
{
  /// <summary>
  /// Checks if the specified image format is supported.
  /// </summary>
  /// <param name="imageFormat">The image format to check (e.g., "jpeg", "png", "webp").</param>
  /// <returns>A Result indicating whether the format is supported (true) or not (false), or an error if the check fails.</returns>
  Result<bool> IsFormatSupported(string imageFormat);

  /// <summary>
  /// Detects the format of the image from the provided stream.
  /// </summary>
  /// <param name="imageStream">The stream containing the image data.</param>
  /// <returns>A Result containing the detected format (e.g., "jpeg", "png") or an empty string if unknown, or an error if detection fails.</returns>
  Result<string> GetImageFormat(Stream imageStream);

  /// <summary>
  /// Gets the size of the image in bytes.
  /// </summary>
  /// <param name="imageStream">The stream containing the image data.</param>
  /// <returns>A Result containing the size of the image in bytes, or an error if the size cannot be determined.</returns>
  Result<long> GetImageSize(Stream imageStream);

  /// <summary>
  /// Compresses the image with the specified quality to the desired format.
  /// </summary>
  /// <param name="imageStream">The stream containing the image data to compress.</param>
  /// <param name="imageFormat">The target format for the compressed image (e.g., "webp", "jpeg", "png"). Defaults to "webp".</param>
  /// <param name="quality">The compression quality (1-100). Defaults to 75.</param>
  /// <returns>A Result containing a stream of the compressed image data, or an error if compression fails.</returns>
  Task<Result<Stream>> CompressImageAsync(
    Stream imageStream,
    string imageFormat = "webp",
    int quality = 75
  );

  /// <summary>
  /// Watermarks the image with the specified text.
  /// </summary>
  /// <param name="imageStream">The stream containing the image data to watermark.</param>
  /// <param name="watermarkText">The text to apply as a watermark.</param>
  /// <returns>A Result containing a stream of the watermarked image data, or an error if watermarking fails or is not implemented.</returns>
  Task<Result<Stream>> WatermarkImageAsync(Stream imageStream, string watermarkText);
}
