using System.Reflection.Metadata;
using Ecommerce.Application.Common.Defaults;
using Ecommerce.Application.Common.Interfaces.Providers.Search.Elastic;
using Ecommerce.Application.Common.Models.Search.Elastic.Documents;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Application.Services.Workers.Elastic;

public class ElasticTaskNotificationHandler(
  IBackgroundTaskQueue taskQueue,
  IElasticSearch elasticSearch,
  ILogger<ElasticTaskNotificationHandler> logger
) : INotificationHandler<ElasticTaskNotification>
{
  public async Task Handle(
    ElasticTaskNotification notification,
    CancellationToken cancellationToken
  )
  {
    if (!await elasticSearch.IsReachableAsync())
      return;

    taskQueue.QueueBackgroundWorkItem(async x =>
    {
      try
      {
        if (notification.ElasticAction == ElasticAction.Index)
        {
          logger.LogInformation("Indexing documents to ElasticSearch.");
          List<ElasticDocumentBase> failed = [];
          foreach (var kvp in notification.IndexDocs)
          {
            var indexName = kvp.Key;

            if (indexName == ElasticIndices.ProductIndex)
              foreach (var doc in kvp.Value)
              {
                var document = (ProductDocument)doc;
                var success = await elasticSearch.IndexAsync(
                  indexName,
                  document,
                  cancellationToken
                );
                if (!success)
                {
                  failed.Add(document);
                  logger.LogError(
                    "Failed to index document \nDocumentId: {DocumentId} \nProductName: {ProductName}\n in index {IndexName}.",
                    document.Id,
                    document.Name,
                    indexName
                  );
                }
              }
          }

          if (failed.Count > 0)
          {
            logger.LogError("Failed to index {Count} documents in ElasticSearch.", failed.Count);
          }
          else
          {
            logger.LogInformation("All documents indexed successfully.");
          }
        }
      }
      catch (Exception ex)
      {
        logger.LogError(
          ex,
          "An error occurred while processing the ElasticTaskNotification: {Message}",
          ex.Message
        );
      }
    });

    await Task.CompletedTask;
  }
}
