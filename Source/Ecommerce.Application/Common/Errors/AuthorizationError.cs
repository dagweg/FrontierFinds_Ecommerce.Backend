using Ecommerce.Domain.Common.Errors;
using FluentResults;

namespace Ecommerce.Application.Common.Errors;

public class AuthorizationError(string Path, string Message)
  : FluentErrorBase(Path, Message, "One or more authorization errors have occured.")
{
    public static Result GetResult(string path, string message)
    {
        return Result.Fail(new AuthorizationError(path, message));
    }
}
