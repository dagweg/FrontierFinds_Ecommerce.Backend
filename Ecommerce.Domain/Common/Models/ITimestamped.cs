namespace Ecommerce.Domain.Common.Models;

public interface ITimeStamped
{
  DateTime CreatedAt { get; }
  DateTime UpdatedAt { get; }
}
