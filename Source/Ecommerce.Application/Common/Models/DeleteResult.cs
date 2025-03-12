namespace Ecommerce.Application.Common.Models;

public class DeleteResult
{
  public required IEnumerable<string> CleanupObjectIds { get; init; }
  public required int TotalItemsDeleted { get; init; }
}
