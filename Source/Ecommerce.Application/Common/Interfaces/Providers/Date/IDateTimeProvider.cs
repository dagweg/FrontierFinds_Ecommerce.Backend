namespace Ecommerce.Application.Common.Interfaces.Providers.Date;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; set; }
}
