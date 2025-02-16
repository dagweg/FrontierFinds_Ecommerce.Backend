using System.Drawing;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Ecommerce.Application.Common.Errors;
using Ecommerce.Application.Common.Interfaces.Storage;
using Ecommerce.Application.Common.Models.Storage;
using FluentResults;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Ecommerce.Infrastructure.Services.Storage;

public class CloudinaryService(
  IOptions<CloudinarySettings> cloudinarySettings,
  ILogger<CloudinaryService> logger,
  ICloudinary cloudinary
) : ICloudinaryService
{
  public string ImagesFolder => $"{cloudinarySettings.Value.Folder}/images";

  public async Task<Result> DeleteImageAsync(DeleteImageParams deleteImageParams)
  {
    var deleteResult = await cloudinary.DestroyAsync(
      new DeletionParams(deleteImageParams.ObjectIdentifier)
    );

    if (deleteResult.Error != null)
    {
      logger.LogError(deleteResult.Error.Message);
      return Result.Fail(new DeleteError(nameof(Image), "Failed to delete image"));
    }

    return Result.Ok();
  }

  public async Task<Result> DeleteImagesAsync(IEnumerable<DeleteImageParams> deleteImagesParams)
  {
    var deleteResult = await cloudinary.DeleteResourcesAsync(
      deleteImagesParams.Select(p => p.ObjectIdentifier).ToArray()
    );

    if (deleteResult.Error != null)
    {
      logger.LogError(deleteResult.Error.Message);
      return Result.Fail(new DeleteError(nameof(Image), "Failed to delete images"));
    }

    return Result.Ok();
  }

  public async Task<Result<UploadImageResult>> UploadImageAsync(UploadImageParams uploadImageParams)
  {
    uploadImageParams.ImageStream.Position = 0; // Reset stream position
    var fileName = uploadImageParams.FileName ?? $"image_{Guid.NewGuid()}.png";
    var uploadResult = await cloudinary.UploadAsync(
      new ImageUploadParams
      {
        File = new FileDescription(fileName, uploadImageParams.ImageStream),
        PublicId = uploadImageParams.ObjectIdentifier ?? Guid.NewGuid().ToString(),
        Folder = ImagesFolder,
      }
    );

    if (uploadResult.Error != null)
    {
      logger.LogError(uploadResult.Error.Message);
      return Result.Fail(new UploadError(nameof(Image), "Failed to upload image"));
    }

    return new UploadImageResult
    {
      Url = uploadResult.SecureUrl.ToString(),
      ObjectIdentifier = uploadResult.PublicId,
    };
  }
}
