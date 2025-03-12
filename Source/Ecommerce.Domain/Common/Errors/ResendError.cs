using FluentResults;

namespace Ecommerce.Domain.Common.Errors;

public class ResendError : FluentErrorBase
{
  public ResendError(string path, string message)
    : base(path, message, $"The resend time hasn't reached yet. Please wait...") { }

  public static Result GetResult(string path, string message)
  {
    return Result.Fail(new ResendError(path, message));
  }
}
