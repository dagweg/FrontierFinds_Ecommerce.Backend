using Ecommerce.Application.Common.Models.Enums;

namespace Ecommerce.Application.UseCases.Common.Interfaces;

public interface ICollectionQueryResult : IDataSourceTrackable
{
  int TotalCount { get; set; }
  int TotalFetchedCount { get; set; }
}

public interface IDataSourceTrackable
{
  DataSourceType_Debug DataSourceType { get; set; }
  string DataSourceId { get; set; }
}
