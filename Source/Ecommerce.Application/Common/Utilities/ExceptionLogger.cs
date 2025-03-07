using Microsoft.Extensions.Logging;

namespace Ecommerce.Application.Common.Utilities;

public static class ExceptionLogger
{
  public static void LogFormattedError(
    this ILogger logger,
    Exception exception,
    string traceId,
    int? statusCode = null
  )
  {
    logger.LogError(
      $"{Environment.NewLine}Exception: {exception.GetType()}"
        + $"{Environment.NewLine}Message: {exception.Message}"
        + $"{Environment.NewLine}StatusCode: {statusCode}"
        + $"{Environment.NewLine}TraceId: {traceId}"
        + $"{Environment.NewLine}StackTrace: {exception.StackTrace}"
    );
  }
}
