using Ecommerce.Application.Common.Interfaces.Logging;
using Serilog;

namespace Ecommerce.Infrastructure.Logging;

public class SerilogService : ILogService
{
    private readonly ILogger _logger;

    public SerilogService()
    {
        _logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("log/log.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();
    }

    public void LogError(string message, Exception ex)
    {
        _logger.Error(message, ex);
    }

    public void LogInformation(string message)
    {
        _logger.Information(message);
    }

    public void LogWarning(string message)
    {
        _logger.Warning(message);
    }
}
