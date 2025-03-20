namespace Ecommerce.Application.Common.Models.Persistence;

public class ChangeTrackerEntryInfo
{
  public string EntityTypeName { get; set; } = default!;
  public string State { get; set; } = default!;
  // You can add more properties if needed, like key values as strings
}
