using FluentResults;

namespace Ecommerce.Application.Common.Errors;

public static class ValidationErrors
{
  public static Result AlreadyExists(string propertyName, string message)
  {
    return Result.Fail(new AlreadyExistsError(propertyName, message));
  }
}
