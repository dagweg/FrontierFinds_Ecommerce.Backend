using FluentResults;

namespace Ecommerce.Domain.Common.Errors;

public class EmptyError : FluentErrorBase
{
  public EmptyError(string path, string message)
    : base(path, message, $"The {path} value must not be empty.") { }

  public static Result GetResult(string path, string message)
  {
    return Result.Fail(new EmptyError(path, message));
  }
}
