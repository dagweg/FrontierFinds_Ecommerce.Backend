using System;
using System.Linq;
using System.Text.RegularExpressions;
using Ecommerce.Domain.Common.Errors;
using FluentResults;

public sealed record CardNumber
{
    public string Value { get; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    private CardNumber() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    private CardNumber(string value)
    {
        Value = value;
    }

    public static Result<CardNumber> Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return EmptyError.GetResult(nameof(CardNumber), "Card number cannot be empty.");

        // Remove spaces and hyphens
        var cleaned = Regex.Replace(value, @"[\s-]+", "");

        // Validate length (common credit card numbers are between 13 and 19 digits)
        if (cleaned.Length < 13 || cleaned.Length > 19)
            return FormatError.GetResult(nameof(CardNumber), "Card number length is invalid.");

        // Ensure all characters are digits
        if (!cleaned.All(char.IsDigit))
            return FormatError.GetResult(nameof(value), "Card number must contain only digits.");

        // Validate using the Luhn algorithm
        if (!IsValidLuhn(cleaned))
            return FormatError.GetResult(nameof(value), "Card number is not valid.");

        return new CardNumber(value);
    }

    private static bool IsValidLuhn(string number)
    {
        int sum = 0;
        bool alternate = false;
        for (int i = number.Length - 1; i >= 0; i--)
        {
            int digit = number[i] - '0';
            if (alternate)
            {
                digit *= 2;
                if (digit > 9)
                    digit -= 9;
            }
            sum += digit;
            alternate = !alternate;
        }
        return (sum % 10 == 0);
    }

    // Returns a masked version of the card number (e.g. "************1234")
    public override string ToString()
    {
        if (Value.Length <= 4)
            return Value;

        return new string('*', Value.Length - 4) + Value.Substring(Value.Length - 4);
    }
}
