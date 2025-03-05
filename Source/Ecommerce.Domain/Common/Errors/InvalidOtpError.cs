using FluentResults;

namespace Ecommerce.Domain.Common.Errors;

public class InvalidOtpError : FluentErrorBase
{
    public InvalidOtpError(string path, string message)
      : base(path, message, $"The {path} is not valid.") { }

    public static Result GetResult(string path, string message)
    {
        return Result.Fail(new InvalidOtpError(path, message));
    }
}
