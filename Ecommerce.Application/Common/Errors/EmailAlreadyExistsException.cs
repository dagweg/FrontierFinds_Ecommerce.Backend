namespace Ecommerce.Application.Common.Errors;

public class EmailAlreadyExistsException : Exception
{
  public const string DefaultMessage = "Email already exists.";

  public EmailAlreadyExistsException(string? message = DefaultMessage)
    : base(message) { }
}
