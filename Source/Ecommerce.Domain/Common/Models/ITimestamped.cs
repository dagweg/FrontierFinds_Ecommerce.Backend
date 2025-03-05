namespace Ecommerce.Domain.Common.Models;

/// <summary>
/// Interface that enforces timestamp properties to entities.
/// </summary>
public interface ITimeStamped
{
    DateTime CreatedAt { get; set; }
    DateTime UpdatedAt { get; set; }
}
