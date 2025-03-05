using Ecommerce.Domain.Common.Errors;
using FluentResults;

namespace Ecommerce.Application.Common.Errors;

public class IncorrectCurrentPasswordError(string Path, string Message)
  : FluentErrorBase(Path, Message, "Credentials are incorrect.")
{
  public static Result GetResult(string path, string message)
  {
    return Result.Fail(new IncorrectCurrentPasswordError(path, message));
  }
}
