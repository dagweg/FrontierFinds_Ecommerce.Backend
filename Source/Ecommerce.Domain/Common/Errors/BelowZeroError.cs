using FluentResults;

namespace Ecommerce.Domain.Common.Errors;

public class BelowZeroError : FluentErrorBase
{
    public BelowZeroError(string path, string message)
      : base(path, message, $"The {path} value must be greater than 0.") { }

    public static Result GetResult(string path, string message)
    {
        return Result.Fail(new BelowZeroError(path, message));
    }
}
