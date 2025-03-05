using Ecommerce.Domain.Common.Errors;
using FluentResults;

public sealed record Month
{
    public int Value { get; }

    private Month() { }

    private Month(int value)
    {
        Value = value;
    }

    public static Result<Month> Create(int month)
    {
        if (month < 1 || month > 12)
            return FormatError.GetResult(nameof(Month), "Month must be a number between 1 and 12.");

        return Result.Ok(new Month(month));
    }

    public override string ToString() => Value.ToString("D2"); // Ensures two-digit format
}
