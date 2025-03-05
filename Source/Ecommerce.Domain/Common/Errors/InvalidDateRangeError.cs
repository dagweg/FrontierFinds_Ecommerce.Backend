using Ecommerce.Domain.Common.Enums;
using Ecommerce.Domain.Common.Errors;
using FluentResults;

namespace Ecommerce.Domain.Common.Errors;

public class InvalidDateRangeError : FluentErrorBase
{
    public InvalidDateRangeError(string path, string message, DateTime startDate, DateTime endDate)
      : base(
        path,
        message,
        $"Invalid date range: {startDate} - {endDate}. Start date should be before end date."
      )
    { }

    public static Result GetResult(string path, string message, DateTime startDate, DateTime endDate)
    {
        return Result.Fail(new InvalidDateRangeError(path, message, startDate, endDate));
    }
}
