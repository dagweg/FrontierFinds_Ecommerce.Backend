using System.Reflection;
using System.Resources;
using Ecommerce.Application.Common;
using Ecommerce.Application.Common.Interfaces.Providers.Localization;

namespace Ecommerce.Infrastructure.Common.Providers.Localization;

public class ValidationMessageProvider : LocalizedMessageProvider, IValidationMessages
{
  public ValidationMessageProvider(string resourcePath, Assembly assembly)
    : base(resourcePath, assembly) { }

  public string PasswordIncorrect => GetMessage(nameof(PasswordIncorrect));

  public string EmailRequired => GetMessage(nameof(EmailRequired));

  public string EmailInvalidFormat => GetMessage(nameof(EmailInvalidFormat));

  public string PasswordRequired => GetMessage(nameof(PasswordRequired));

  public string ConfirmPasswordRequired => GetMessage(nameof(ConfirmPasswordRequired));

  public string PasswordsDoNotMatch => GetMessage(nameof(PasswordsDoNotMatch));

  public string NameRequired => GetMessage(nameof(NameRequired));

  public string FirstNameRequired => GetMessage(nameof(FirstNameRequired));

  public string MiddleNameRequired => GetMessage(nameof(MiddleNameRequired));

  public string LastNameRequired => GetMessage(nameof(LastNameRequired));

  public string PhoneRequired => GetMessage(nameof(PhoneRequired));

  public string ProductNotFound => GetMessage(nameof(ProductNotFound));
}
