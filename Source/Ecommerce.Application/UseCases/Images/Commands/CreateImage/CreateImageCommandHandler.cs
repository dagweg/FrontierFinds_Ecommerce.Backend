using Ecommerce.Application.Common.Interfaces.Processors;
using Ecommerce.Application.Common.Interfaces.Storage;
using Ecommerce.Application.Common.Models.Storage;
using Ecommerce.Application.UseCases.Images.Common;
using FluentResults;
using MediatR;

namespace Ecommerce.Application.UseCases.Images.CreateImage;

public class CreateImageCommandHandler(
  ICloudStorageService cloudStorageService,
  IExternalResourceTracker externalResourceTracker,
  IImageProcessor imageProcessor
) : IRequestHandler<CreateImageCommand, Result<ImageResult>>
{
    public async Task<Result<ImageResult>> Handle(
      CreateImageCommand request,
      CancellationToken cancellationToken
    )
    {
        // compress the image
        var compressionResult = await imageProcessor.CompressImageAsync(request.ImageStream);

        if (compressionResult.IsFailed)
            return compressionResult.ToResult();

        Stream compressedImageStream = compressionResult.Value;

        var uploadResult = await cloudStorageService.UploadImageAsync(
          new UploadImageParams { ImageStream = compressedImageStream, FileName = request.FileName }
        );

        if (uploadResult.IsFailed)
            return uploadResult.ToResult();

        // Track the uploaded image. For rollback purposes incase of failure.
        externalResourceTracker.AddUploadedImage(uploadResult.Value.ObjectIdentifier);

        return Result.Ok(
          new ImageResult
          {
              Url = uploadResult.Value.Url,
              ObjectIdentifier = uploadResult.Value.ObjectIdentifier,
          }
        );
    }
}
