using FluentResults;

namespace Ecommerce.Domain.Common.Errors;

public class FormatError : FluentErrorBase
{
  public FormatError(string path, string message)
    : base(path, message, $"The {path} value is in an invalid format.") { }

  public static Result GetResult(string path, string message)
  {
    return Result.Fail(new FormatError(path, message));
  }
}
