using Ecommerce.Application.Common.Models.Storage;
using FluentResults;

namespace Ecommerce.Application.Common.Interfaces.Storage;

public interface ICloudStorageService
{
  /// <summary>
  /// Uploads an image to the cloud storage
  /// </summary>
  /// <param name="uploadImageParams"></param>
  /// <returns>
  //  <see cref="UploadImageResult"/>
  // </returns>
  Task<Result<UploadImageResult>> UploadImageAsync(UploadImageParams uploadImageParams);

  /// <summary>
  /// Deletes an image from the cloud storage
  /// </summary>
  /// <param name="deleteImageParams"></param>
  /// <returns></returns>
  Task<Result> DeleteImageAsync(DeleteImageParams deleteImageParams);

  /// <summary>
  /// Deletes an image from the cloud storage
  /// </summary>
  /// <param name="deleteImageParams"></param>
  /// <returns></returns>
  Task<Result> DeleteImagesAsync(IEnumerable<DeleteImageParams> deleteImagesParams);
}
