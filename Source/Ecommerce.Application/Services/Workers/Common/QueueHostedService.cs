namespace Ecommerce.Application.Services.Workers.Common;

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

public class QueuedHostedService : BackgroundService
{
  private readonly IBackgroundTaskQueue _taskQueue;
  private readonly ILogger<QueuedHostedService> _logger;

  public QueuedHostedService(IBackgroundTaskQueue taskQueue, ILogger<QueuedHostedService> logger)
  {
    _taskQueue = taskQueue;
    _logger = logger;
  }

  protected override async Task ExecuteAsync(CancellationToken stoppingToken)
  {
    _logger.LogInformation("Queued Hosted Service is starting.");

    while (!stoppingToken.IsCancellationRequested)
    {
      var workItem = await _taskQueue.DequeueAsync(stoppingToken);

      try
      {
        await workItem(stoppingToken);
      }
      catch (Exception ex)
      {
        _logger.LogError(ex, "Error occurred executing task.");
      }
    }

    _logger.LogInformation("Queued Hosted Service is stopping.");
  }
}
