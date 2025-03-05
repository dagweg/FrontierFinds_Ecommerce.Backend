using System.Drawing.Imaging;
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
      logger.LogError("Quality parameter is out of range [1,100]");
      return InternalError.GetResult();
    }

    Stream outStream = new MemoryStream();

    using (Image image = Image.Load(imageStream))
    {
      Result<IImageEncoder> imageEncoder = imageFormat.ToLower() switch
      {
        "jpeg" => new JpegEncoder { Quality = quality },
        "png" => new PngEncoder { CompressionLevel = (PngCompressionLevel)((quality - 1) / 10) },
        "webp" => new WebpEncoder { Quality = quality },
        _ => InternalError.GetResult(),
      };

      if (imageEncoder.IsFailed)
      {
        logger.LogError($"{imageFormat} is unsupported.");
        return imageEncoder.ToResult();
      }

      await image.SaveAsync(outStream, imageEncoder.Value);
    }

    return outStream;
  }

  public Result<string> GetImageFormat(Stream imageStream)
  {
    var format = Image.DetectFormat(imageStream);
    if (format is null)
    {
      logger.LogError($"Image stream has unsupported format");
      return NotFoundError.GetResult(
        nameof(imageStream),
        "The stream has unsupported format. Format couldn't be determined."
      );
    }

    return format.Name.ToLowerInvariant();
  }

  public Result<long> GetImageSize(Stream imageStream)
  {
    return imageStream.Length;
  }

  public Task<Result<Stream>> WatermarkImageAsync(Stream imageStream, string watermarkText)
  {
    throw new NotImplementedException();
  }
}
