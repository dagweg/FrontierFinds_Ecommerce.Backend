using System.Resources;
using Ecommerce.Application.Common;
using Ecommerce.Application.Common.Interfaces;

namespace Ecommerce.Infrastructure.Common;

/// <summary>
/// Provides methods to access validation messages from the
/// <see cref="ValidationMessages"/> resource file.
/// It uses the utility class defined in <see cref="ValidationMessageKeys"/>
/// to retrieve validation messages based on keys.
/// </summary>
public class ValidationMessageProvider : IValidationMessageProvider
{
  private readonly ResourceManager _resourceManager;

  public string GetMessage(string key)
  {
    return _resourceManager.GetString(key, System.Globalization.CultureInfo.CurrentCulture) ?? "";
  }

  public ValidationMessageProvider()
  {
    _resourceManager = new ResourceManager(
      "Ecommerce.Application.Common.Resources.ValidationMessages",
      ApplicationAssembly.Assembly
    );
  }
}
