namespace Ecommerce.Domain.OrderAggregate.ValueObjects;

using System;
using System.Linq;
using Ecommerce.Domain.Common.Errors;
using FluentResults;

public sealed record CVV
{
    public string Value { get; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private CVV() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    private CVV(string value)
    {
        Value = value;
    }

    public static Result<CVV> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return EmptyError.GetResult(nameof(CVV), "CVV cannot be empty.");

        // Ensure all characters are digits
        if (!value.All(char.IsDigit))
            return FormatError.GetResult(nameof(CVV), "CVV must contain only digits.");

        // Validate length (typically 3 or 4 digits)
        if (value.Length < 3 || value.Length > 4)
            return FormatError.GetResult(nameof(CVV), "CVV must be 3 or 4 digits long.");

        return new CVV(value);
    }

    public override string ToString()
    {
        // Mask the CVV for additional security (e.g., "***")
        return new string('*', Value.Length);
    }
}
