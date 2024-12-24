namespace Ecommerce.Application.Common.Errors;

public class EmailNotFoundException : Exception
{
  public const string DefaultMessage = "Email not found.";

  public EmailNotFoundException(string? message = DefaultMessage)
    : base(message) { }
}
