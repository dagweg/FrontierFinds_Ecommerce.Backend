namespace Ecommerce.Application.UseCases.Common.Interfaces;

public interface ICollectionQueryResult
{
  int TotalCount { get; set; }
  int TotalFetchedCount { get; set; }
}
