using System;
using System.IO;
using System.Threading.Tasks;
using Ecommerce.Application.Common.Errors;
using Ecommerce.Application.Common.Interfaces.Processors;
using FluentResults;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Formats.Webp;

namespace Ecommerce.Infrastructure.Services.Processors;

public class ImageProcessor(ILogger<ImageProcessor> logger) : IImageProcessor
{
  public async Task<Result<Stream>> CompressImageAsync(
    Stream imageStream,
    string imageFormat = "webp",
    int quality = 75
  )
  {
    if (quality < 1 || quality > 100)
    {
      logger.LogError("Quality parameter is out of range [1,100]: {Quality}", quality);
      return Result.Fail(new InternalError("Quality must be between 1 and 100"));
    }

    if (imageStream == null || !imageStream.CanRead)
    {
      logger.LogError("Invalid or unreadable image stream provided");
      return Result.Fail(new InternalError("Invalid image stream"));
    }

    // Reset stream position to ensure it can be read
    if (imageStream.CanSeek)
    {
      imageStream.Position = 0;
    }

    try
    {
      using var image = await Image.LoadAsync(imageStream);
      var outStream = new MemoryStream();

      Result<IImageEncoder> imageEncoder = imageFormat.ToLowerInvariant() switch
      {
        "jpeg" => Result.Ok<IImageEncoder>(new JpegEncoder { Quality = quality }),
        "png" => Result.Ok<IImageEncoder>(
          new PngEncoder { CompressionLevel = MapQualityToPngCompression(quality) }
        ),
        "webp" => Result.Ok<IImageEncoder>(new WebpEncoder { Quality = quality }),
        _ => Result.Fail(new InternalError($"Unsupported image format: {imageFormat}")),
      };

      if (imageEncoder.IsFailed)
      {
        logger.LogError("Unsupported image format: {ImageFormat}", imageFormat);
        return imageEncoder.ToResult();
      }

      await image.SaveAsync(outStream, imageEncoder.Value);
      outStream.Position = 0; // Reset position for the caller
      return Result.Ok(outStream as Stream);
    }
    catch (Exception ex)
    {
      logger.LogError(ex, "Failed to compress image");
      return Result.Fail(new InternalError("Image compression failed"));
    }
  }

  public Result<string> GetImageFormat(Stream imageStream)
  {
    if (imageStream == null || !imageStream.CanRead)
    {
      logger.LogError("Invalid or unreadable image stream provided");
      return Result.Fail(new InternalError("Invalid image stream"));
    }

    if (imageStream.CanSeek)
    {
      imageStream.Position = 0;
    }

    try
    {
      var format = Image.DetectFormat(imageStream);
      var formatName = format?.Name.ToLowerInvariant() switch
      {
        "jpeg" => "image/jpeg",
        "png" => "image/png",
        "webp" => "image/webp",
        _ => null, // Unknown format
      };
      logger.LogInformation("Detected image format: {Format}", formatName ?? "unknown");
      return Result.Ok(formatName ?? string.Empty);
    }
    catch (Exception ex)
    {
      logger.LogError(ex, "Failed to detect image format");
      return Result.Fail(new InternalError("Failed to detect image format"));
    }
  }

  public Result<long> GetImageSize(Stream imageStream)
  {
    if (imageStream == null || !imageStream.CanRead)
    {
      logger.LogError("Invalid or unreadable image stream provided");
      return Result.Fail(new InternalError("Invalid image stream"));
    }

    try
    {
      return Result.Ok(imageStream.Length);
    }
    catch (Exception ex)
    {
      logger.LogError(ex, "Failed to get image size");
      return Result.Fail(new InternalError("Failed to get image size"));
    }
  }

  public Result<bool> IsFormatSupported(string imageFormat)
  {
    if (string.IsNullOrEmpty(imageFormat))
    {
      logger.LogError("Image format cannot be null or empty");
      return Result.Fail(new InternalError("Image format cannot be null or empty"));
    }

    var isSupported = Domain
      .Common.Entities.Image.SUPPORTED_FORMATS.Split(",")
      .Contains(imageFormat.ToLowerInvariant());

    logger.LogInformation(
      "Checking format support: {ImageFormat} -> {IsSupported}",
      imageFormat,
      isSupported
    );
    return Result.Ok(isSupported);
  }

  public Task<Result<Stream>> WatermarkImageAsync(Stream imageStream, string watermarkText)
  {
    logger.LogWarning("WatermarkImageAsync is not implemented");
    return Task.FromResult(
      Result.Fail<Stream>(new InternalError("Watermarking is not implemented"))
    );
  }

  private static PngCompressionLevel MapQualityToPngCompression(int quality)
  {
    // Map 1-100 quality to 0-9 compression level (higher quality -> lower compression)
    return (PngCompressionLevel)Math.Min(9, Math.Max(0, (100 - quality) / 11));
  }
}
