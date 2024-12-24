namespace Ecommerce.Application.Common.Interfaces;

/// <summary>
/// This interface defines methods that will help access
/// validation messages from a resource file.
/// </summary>
public interface IValidationMessageProvider
{
  string GetMessage(string key);
}
