namespace Ecommerce.Application.Common.Errors;

public class PasswordIncorrectException : Exception
{
  public const string DefaultMessage = "Password is incorrect.";

  public PasswordIncorrectException(string? message = DefaultMessage)
    : base(message) { }
}
