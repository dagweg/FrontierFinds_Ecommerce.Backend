using Ecommerce.Domain.Common.Errors;
using FluentResults;

namespace Ecommerce.Application.Common.Errors;

public class DeleteError(string Path, string Message)
  : FluentErrorBase(Path, Message, "Failed trying to delete the file.")
{
    public static Result GetResult(string path, string message)
    {
        return Result.Fail(new DeleteError(path, message));
    }
}
