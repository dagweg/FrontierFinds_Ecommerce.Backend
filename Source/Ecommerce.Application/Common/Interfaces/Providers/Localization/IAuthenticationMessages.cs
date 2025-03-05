namespace Ecommerce.Application.Common.Interfaces.Providers.Localization;

/// <summary>
/// Interface for messages related to authentication.
/// </summary>
public interface IAuthenticationMessages
{
    string EmailOrPasswordIncorrect { get; }
    string UserAlreadyExists { get; }
    string UserNotFound { get; }
}
