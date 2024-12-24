namespace Ecommerce.Application.Common.Errors;

public class UserNotFoundException : Exception
{
  public const string DefaultMessage = "User is not found.";

  public UserNotFoundException(string? message = DefaultMessage)
    : base(message) { }
}
