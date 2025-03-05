using System.Globalization;
using System.Reflection;
using System.Resources;
using Ecommerce.Application.Common;
using Ecommerce.Application.Common.Interfaces.Providers.Localization;

namespace Ecommerce.Infrastructure.Common.Providers.Localization;

public class AuthenticationMessageProvider : LocalizedMessageProvider, IAuthenticationMessages
{
    public AuthenticationMessageProvider(string basePath, Assembly assembly)
      : base(basePath, assembly) { }

    public string EmailOrPasswordIncorrect => GetMessage("EmailOrPasswordIncorrect");

    public string UserAlreadyExists => GetMessage("UserAlreadyExists");

    public string UserNotFound => GetMessage("UserNotFound");
}
