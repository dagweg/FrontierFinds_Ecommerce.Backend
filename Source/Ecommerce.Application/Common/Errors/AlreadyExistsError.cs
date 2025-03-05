using Ecommerce.Domain.Common.Errors;
using FluentResults;

namespace Ecommerce.Application.Common.Errors;

public class AlreadyExistsError(string path, string message)
  : FluentErrorBase(path, message, $"A resource already exists.")
{
  public static Result GetResult(string path, string message)
  {
    return Result.Fail(new AlreadyExistsError(path, message));
  }
}
