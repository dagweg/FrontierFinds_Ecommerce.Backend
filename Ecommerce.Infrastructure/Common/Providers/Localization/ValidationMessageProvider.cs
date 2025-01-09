using System.Reflection;
using System.Resources;
using Ecommerce.Application.Common;
using Ecommerce.Application.Common.Interfaces.Providers.Localization;

namespace Ecommerce.Infrastructure.Common.Providers.Localization;

public class ValidationMessageProvider : LocalizedMessageProvider, IValidationMessages
{
  public ValidationMessageProvider(string resourcePath, Assembly assembly)
    : base(resourcePath, assembly) { }

  public string PasswordIncorrect => GetMessage("PasswordIncorrect");

  public string EmailRequired => GetMessage("EmailRequired");

  public string EmailInvalidFormat => GetMessage("EmailInvalidFormat");

  public string PasswordRequired => GetMessage("PasswordRequired");

  public string ConfirmPasswordRequired => GetMessage("ConfirmPasswordRequired");

  public string PasswordsDoNotMatch => GetMessage("PasswordsDoNotMatch");

  public string NameRequired => GetMessage("NameRequired");

  public string FirstNameRequired => GetMessage("FirstNameRequired");

  public string MiddleNameRequired => GetMessage("MiddleNameRequired");

  public string LastNameRequired => GetMessage("LastNameRequired");

  public string PhoneRequired => GetMessage("PhoneRequired");

  public string CountryCodeRequired => GetMessage("CountryCodeRequired");
}
