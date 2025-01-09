namespace Ecommerce.Application.Common.Errors;

public class AuthenticationError(string Path, string Message)
  : FluentErrorBase(Path, Message, "One or more authentication errors have occured.");
