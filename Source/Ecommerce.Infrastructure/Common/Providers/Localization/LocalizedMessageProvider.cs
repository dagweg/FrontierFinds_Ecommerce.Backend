using System.Globalization;
using System.Reflection;
using System.Resources;
using Ecommerce.Application.Common.Interfaces.Providers.Localization;

namespace Ecommerce.Infrastructure.Common.Providers.Localization;

public class LocalizedMessageProvider : ILocalizedMessageProvider
{
    private readonly ResourceManager _resourceManager;

    public LocalizedMessageProvider(string resourcePath, Assembly assembly)
    {
        _resourceManager = new ResourceManager(resourcePath, assembly);
    }

    public string GetMessage(string key, CultureInfo? cultureInfo = null)
    {
        return _resourceManager.GetString(key, cultureInfo ?? CultureInfo.CurrentCulture)
          ?? "Failed to retrieve Localized Message";
    }
}
