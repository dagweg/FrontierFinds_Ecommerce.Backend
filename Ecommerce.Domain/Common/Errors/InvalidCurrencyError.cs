using Ecommerce.Domain.Common.Enums;
using Ecommerce.Domain.Common.Errors;
using FluentResults;

namespace Ecommerce.Domain.Common.Errors;

public class InvalidCurrencyError : FluentErrorBase
{
  public InvalidCurrencyError(string path, string message, string currency)
    : base(
      path,
      message,
      $"Invalid currency: {currency}. Available currencies are {string.Join(", ", Enum.GetNames<Currency>().Skip(1))}"
    ) { }

  public static Result GetResult(string path, string message, string currency)
  {
    return Result.Fail(new InvalidCurrencyError(path, message, currency));
  }
}
