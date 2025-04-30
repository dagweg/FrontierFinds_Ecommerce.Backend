using Ecommerce.Application.Common.Models.Search.Elastic.Documents;
using MediatR;

namespace Ecommerce.Application.Services.Workers.Elastic;

public record ElasticTaskNotification : INotification
{
  public required Dictionary<string, ElasticDocumentBase> IndexDocs = new();
  public required ElasticAction ElasticAction { get; init; }
}
