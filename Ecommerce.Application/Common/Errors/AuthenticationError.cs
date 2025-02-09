using Ecommerce.Domain.Common.Errors;
using FluentResults;

namespace Ecommerce.Application.Common.Errors;

public class AuthenticationError(string Path, string Message)
  : FluentErrorBase(Path, Message, "One or more authentication errors have occured.")
{
  public static Result GetResult(string path, string message)
  {
    return Result.Fail(new AuthenticationError(path, message));
  }
}
