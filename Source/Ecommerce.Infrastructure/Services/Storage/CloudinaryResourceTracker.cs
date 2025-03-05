using Ecommerce.Application.Common.Interfaces.Storage;
using Ecommerce.Application.Common.Models.Storage;

namespace Ecommerce.Application.Services.Storage;

public class CloudinaryResourceTracker(ICloudinaryService cloudinaryService)
  : ICloudinaryResourceTracker
{
    public List<string> UploadedImagesPublicIds { get; } = new();

    /// <summary>
    /// Add the uploaded image's objectIdentifier (public_id) to the list of uploaded images
    /// </summary>
    /// <param name="objectIdentifier"></param>
    public void AddUploadedImage(string objectIdentifier)
    {
        UploadedImagesPublicIds.Add(objectIdentifier);
    }

    public async Task RollbackAsync()
    {
        // Delete all uploaded images
        await cloudinaryService.DeleteImagesAsync(
          UploadedImagesPublicIds.Select(p => new DeleteImageParams(p))
        );
        UploadedImagesPublicIds.Clear(); // Clear the list
    }
}
