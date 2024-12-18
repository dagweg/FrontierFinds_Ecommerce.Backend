namespace Ecommerce.Application.Common.Interfaces.Logging;

public interface ILogService
{
    void LogInformation(string message);
    void LogWarning(string message);
    void LogError(string message, Exception ex);
}
