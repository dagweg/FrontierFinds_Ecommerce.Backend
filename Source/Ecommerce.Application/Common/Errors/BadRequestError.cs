using Ecommerce.Domain.Common.Errors;
using FluentResults;

namespace Ecommerce.Application.Common.Errors;

public class BadRequestError(string Path, string Message)
  : FluentErrorBase(
    Path,
    Message,
    "The request was not valid. Please check the request and try again."
  )
{
  public static Result GetResult(string path, string message)
  {
    return Result.Fail(new BadRequestError(path, message));
  }
}
