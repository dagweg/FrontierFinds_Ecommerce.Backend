using Ecommerce.Domain.Common.Errors;
using FluentResults;

namespace Ecommerce.Application.Common.Errors;

public class InternalError(string? message = null)
  : FluentErrorBase("Error", message ?? "Internal Error", "An internal server error has occured.")
{
    public static Result GetResult(string? message = null)
    {
        return Result.Fail(new InternalError(message));
    }
}
