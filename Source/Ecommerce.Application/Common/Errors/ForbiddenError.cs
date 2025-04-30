using Ecommerce.Domain.Common.Errors;
using FluentResults;

namespace Ecommerce.Application.Common.Errors;

public class ForbiddenError(string Path, string Message)
  : FluentErrorBase(Path, Message, "You are not allowed to perform this action at the moment.")
{
  public static Result GetResult(string path, string message)
  {
    return Result.Fail(new ForbiddenError(path, message));
  }
}
