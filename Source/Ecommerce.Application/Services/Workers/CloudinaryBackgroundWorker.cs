using Ecommerce.Application.Common.Interfaces.Storage;
using Ecommerce.Application.Common.Models.Storage;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Application.Services.Workers;

public class CloudinaryBackgroundWorker : INotificationHandler<CloudinaryTaskNotification>
{
  private readonly IBackgroundTaskQueue _taskQueue;
  private readonly ICloudinaryService _cloudinaryService;
  private readonly ILogger<CloudinaryBackgroundWorker> _logger;

  public CloudinaryBackgroundWorker(
    IBackgroundTaskQueue taskQueue,
    ICloudinaryService cloudinaryService,
    ILogger<CloudinaryBackgroundWorker> logger
  )
  {
    _taskQueue = taskQueue;
    _cloudinaryService = cloudinaryService;
    _logger = logger;
  }

  public async Task Handle(
    CloudinaryTaskNotification notification,
    CancellationToken cancellationToken
  )
  {
    _taskQueue.QueueBackgroundWorkItem(
      async (c) =>
      {
        try
        {
          if (notification.CloudinaryAction == CloudinaryAction.Delete)
          {
            await _cloudinaryService.DeleteImagesAsync(
              notification.ObjectIds.Select(oi => new DeleteFileParams(oi))
            );
          }
          _logger.LogInformation("Cloudinary task completed.");
        }
        catch (System.Exception ex)
        {
          _logger.LogError("Error occurred while processing cloudinary task bkg work. \n{@ex}", ex);
        }
      }
    );

    await Task.CompletedTask;
  }
}
