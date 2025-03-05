using System.Globalization;

namespace Ecommerce.Application.Common.Interfaces.Providers.Localization;

/// <summary>
/// Interface implemented by all Message Providers that provide localized messages.
/// </summary>
public interface ILocalizedMessageProvider
{
    string GetMessage(string key, CultureInfo cultureInfo);
}
