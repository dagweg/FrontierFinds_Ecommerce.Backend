namespace Ecommerce.Application.Common.Interfaces.Providers.Localization;

/// <summary>
/// Provides the interface for validation messages for the application.
/// </summary>
public interface IValidationMessages
{
  string PasswordIncorrect { get; }
  string EmailRequired { get; }
  string EmailInvalidFormat { get; }
  string PasswordRequired { get; }
  string ConfirmPasswordRequired { get; }
  string PasswordsDoNotMatch { get; }
  string NameRequired { get; }
  string FirstNameRequired { get; }
  string MiddleNameRequired { get; }
  string LastNameRequired { get; }
  string PhoneRequired { get; }
  string CountryCodeRequired { get; }
  string ProductNotFound { get; }
}
