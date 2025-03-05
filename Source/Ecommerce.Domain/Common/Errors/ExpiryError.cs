using FluentResults;

namespace Ecommerce.Domain.Common.Errors;

public class ExpiryError : FluentErrorBase
{
  public ExpiryError(string path, string message)
    : base(path, message, $"The {path} has already expired.") { }

  public static Result GetResult(string path, string message)
  {
    return Result.Fail(new ExpiryError(path, message));
  }
}
