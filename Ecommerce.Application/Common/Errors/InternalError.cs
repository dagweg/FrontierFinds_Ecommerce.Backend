using Ecommerce.Domain.Common.Errors;
using FluentResults;

namespace Ecommerce.Application.Common.Errors;

public class InternalError(string Path, string Message)
  : FluentErrorBase(Path, Message, "An internal server error has occured.")
{
  public static Result GetResult(string path, string message)
  {
    return Result.Fail(new InternalError(path, message));
  }
}
