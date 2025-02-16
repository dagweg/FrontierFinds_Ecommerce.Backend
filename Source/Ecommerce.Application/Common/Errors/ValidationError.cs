using Ecommerce.Domain.Common.Errors;
using FluentResults;

namespace Ecommerce.Application.Common.Errors;

public class ValidationError : FluentErrorBase
{
  public ValidationError(string Path, string Message)
    : base(Path, Message, "One or move validation errors have occured.") { }

  public static Result GetResult(string path, string message)
  {
    return Result.Fail(new ValidationError(path, message));
  }
}
