namespace Ecommerce.Application.Common;

/// <summary>
/// A utility class that defines keys to map to fields found
/// inside the resource file <see cref="ValidationMessages.resx"/>.
/// Once you add relevant keys inside the resource file
/// add the keys here to access the message like so:
/// <code>_validationMessageProvider.GetMessage(ValidationMessageKeys.YOUR_KEY)</code>
/// </summary>
public static class ValidationMessageKeys
{
  public const string EmailRequired = "EmailRequired";
  public const string PasswordRequired = "PasswordRequired";
  public const string NameRequired = "NameRequired";
  public const string FirstNameRequired = "FirstNameRequired";
  public const string MiddleNameRequired = "MiddleNameRequired";
  public const string LastNameRequired = "LastNameRequired";
  public const string PhoneRequired = "PhoneRequired";
  public const string CountryCodeRequired = "CountryCodeRequired";
}
