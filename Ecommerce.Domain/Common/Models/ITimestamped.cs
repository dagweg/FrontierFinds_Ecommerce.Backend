namespace Ecommerce.Domain.Common.Models;

/// <summary>
/// Interface that enforces timestamp properties to entities.
/// </summary>
public interface ITimeStamped
{
  DateTime CreatedAt { get; }
  DateTime UpdatedAt { get; }
}
