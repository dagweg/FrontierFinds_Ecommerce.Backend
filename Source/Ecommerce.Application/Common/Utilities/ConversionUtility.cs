using Ecommerce.Application.Common.Errors;
using FluentResults;

namespace Ecommerce.Application.Common.Utilities;

public static class ConversionUtility
{
    /// <summary>
    /// Convert a string to a GUID
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static Result<Guid> ToGuid(string value)
    {
        var convert = Guid.TryParse(value, out var result);

        if (!convert)
        {
            return Result.Fail(new ValidationError("value", "Invalid GUID string"));
        }

        return Result.Ok(result);
    }

    public static Result<int[]> ToIntArray(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return ValidationError.GetResult(nameof(value), "Input string cannot be null or empty.");
        }

        char[] chars = value.ToCharArray();
        int[] result = new int[chars.Length];

        for (int i = 0; i < chars.Length; i++)
        {
            if (!char.IsDigit(chars[i]))
            {
                return ValidationError.GetResult(nameof(value), "Input string must contain only digits.");
            }

            result[i] = int.Parse(chars[i].ToString());
        }

        return Result.Ok(result);
    }

    public static string PascalToSpacedPascal(this string pascalCase)
    {
        if (string.IsNullOrEmpty(pascalCase))
        {
            return pascalCase; // Handle null or empty input
        }

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(pascalCase[0]); // Append the first character
        for (int i = 1; i < pascalCase.Length; i++)
        {
            if (char.IsUpper(pascalCase[i]))
            {
                sb.Append(' ');
            }
            sb.Append(pascalCase[i]);
        }
        return sb.ToString();
    }
}
