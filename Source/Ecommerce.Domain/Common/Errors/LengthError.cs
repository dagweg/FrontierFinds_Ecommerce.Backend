using FluentResults;

namespace Ecommerce.Domain.Common.Errors;

public class LengthError : FluentErrorBase
{
    public LengthError(string path, string message)
      : base(path, message, $"The {path} doesn't satisfy the length constraint.") { }

    public static Result GetResult(string path, string message)
    {
        return Result.Fail(new LengthError(path, message));
    }
}
