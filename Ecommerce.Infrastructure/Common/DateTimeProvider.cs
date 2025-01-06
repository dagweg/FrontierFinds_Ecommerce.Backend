using System;
using Ecommerce.Application.Common.Interfaces;

namespace Ecommerce.Infrastructure.Common;

public class DateTimeProvider : IDateTimeProvider
{
  public DateTime UtcNow { get; set; } = DateTime.UtcNow;
}
