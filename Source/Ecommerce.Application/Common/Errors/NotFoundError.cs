using Ecommerce.Domain.Common.Errors;
using FluentResults;

namespace Ecommerce.Application.Common.Errors;

public class NotFoundError(string Path, string Message)
  : FluentErrorBase(Path, Message, "Resource not found.")
{
    public static Result GetResult(string path, string message)
    {
        return Result.Fail(new NotFoundError(path, message));
    }
}
