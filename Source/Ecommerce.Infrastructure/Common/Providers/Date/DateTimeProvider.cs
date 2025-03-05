namespace Ecommerce.Infrastructure.Common.Providers;

using System;
using Ecommerce.Application.Common.Interfaces.Providers.Date;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow { get; set; } = DateTime.UtcNow;
}
