namespace Ecommerce.Application.Common.Interfaces;

public interface IDateTimeProvider
{
  DateTime UtcNow { get; set; }
}
