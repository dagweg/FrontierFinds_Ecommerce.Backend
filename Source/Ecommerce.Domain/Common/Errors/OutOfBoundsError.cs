using FluentResults;

namespace Ecommerce.Domain.Common.Errors;

public class OutOfBoundsError<T> : FluentErrorBase
{
    /// <summary>
    /// An error that occurs when a value is out of bounds of a specified range.
    /// </summary>
    /// <param name="path"></param>
    /// <param name="lowerBound"></param>
    /// <param name="upperBound"></param>
    public OutOfBoundsError(string path, string message, T lowerBound, T upperBound)
      : base(path, message, $"The {path} value must be between {lowerBound} and {upperBound}.") { }

    public static Result GetResult(string path, string message, T lowerBound, T upperBound)
    {
        return Result.Fail(new OutOfBoundsError<T>(path, message, lowerBound, upperBound));
    }
}
