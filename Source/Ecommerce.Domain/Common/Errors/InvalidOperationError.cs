using FluentResults;

namespace Ecommerce.Domain.Common.Errors;

public class InvalidOperationError : FluentErrorBase
{
  public InvalidOperationError(string path, string message)
    : base(path, message, $"You are trying to perform an invalid operation.") { }

  public static Result GetResult(string path, string message)
  {
    return Result.Fail(new InvalidOperationError(path, message));
  }
}
