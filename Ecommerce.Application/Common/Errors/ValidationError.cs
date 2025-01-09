namespace Ecommerce.Application.Common.Errors;

public class ValidationError : FluentErrorBase
{
  public ValidationError(string Path, string Message)
    : base(Path, Message, "One or move validation errors have occured.") { }
}
