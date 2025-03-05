using Ecommerce.Domain.Common.Errors;
using FluentResults;

namespace Ecommerce.Application.Common.Errors;

public class PasswordMatchError(string Path, string Message)
  : FluentErrorBase(Path, Message, "Invalid password provided.")
{
    public static Result GetResult(string path, string message)
    {
        return Result.Fail(new PasswordMatchError(path, message));
    }
}
