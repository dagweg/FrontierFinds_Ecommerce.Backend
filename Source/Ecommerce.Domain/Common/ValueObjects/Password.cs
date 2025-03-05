namespace Ecommerce.Domain.Common.ValueObjects;

using System.Text.RegularExpressions;
using Ecommerce.Domain.Common.Errors;
using Ecommerce.Domain.Common.Models;
using FluentResults;

public sealed class Password : ValueObject
{
    public string ValueHash { get; set; }

    public static Password Empty => new(string.Empty);

    private const int MIN_PASSWORD_LENGTH = 8;
    private const int MIN_SPECIAL_CHARS = 1;
    private const int MIN_LOWERCASE_CHARS = 1;
    private const int MIN_UPPERCASE_CHARS = 1;
    private const int MIN_NUMERIC = 1;

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
    public Password() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

    private Password(string passwordHash)
    {
        ValueHash = passwordHash;
    }

    public static Password CreateRandom()
    {
        string password = string.Empty;
        Random random = new();
        for (int i = 0; i < MIN_PASSWORD_LENGTH; i++)
        {
            char c = (char)random.Next(33, 126);
            password += c;
        }
        return new Password(password);
    }

    [Obsolete(
      "This is meant to be used for configuration only. Use Create(passwordHash, password) instead."
    )]
    public static Password Create(string passwordHash) => new(passwordHash);

    public static Result<Password> Create(string passwordHash, string password)
    {
        var result = PassesRequirement(password);
        if (result.IsFailed)
            return result;
        return new Password(passwordHash);
    }

    public static Result PassesRequirement(string password)
    {
        var trimmed = password.Trim();
        if (trimmed.Length < MIN_PASSWORD_LENGTH)
            return LengthError.GetResult(
              nameof(password),
              $"Min password length is {MIN_PASSWORD_LENGTH}"
            );

        int s = MIN_SPECIAL_CHARS,
          l = MIN_LOWERCASE_CHARS,
          u = MIN_UPPERCASE_CHARS,
          n = MIN_NUMERIC;

        foreach (char c in trimmed)
        {
            if (char.IsLower(c))
                l -= 1;
            else if (char.IsUpper(c))
                u -= 1;
            else if (SpecialChars.Contains(c))
                s -= 1;
            else if (char.IsNumber(c))
                n -= 1;
        }

        if (l > 0)
            return LengthError.GetResult(
              nameof(password),
              $"Password should have at least {MIN_LOWERCASE_CHARS} lowercase characters."
            );
        else if (u > 0)
            return LengthError.GetResult(
              nameof(password),
              $"Password should have at least {MIN_UPPERCASE_CHARS} uppercase characters."
            );
        else if (s > 0)
            return LengthError.GetResult(
              nameof(password),
              $"Password should have at least {MIN_SPECIAL_CHARS} special characters."
            );
        else if (n > 0)
            return LengthError.GetResult(
              nameof(password),
              $"Password should have at least {MIN_NUMERIC} numeric characters."
            );

        return Result.Ok();
    }

    public static implicit operator string(Password password) => password.ValueHash;

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return ValueHash;
    }

    private static readonly HashSet<char> SpecialChars = new HashSet<char>
  {
    '~',
    '!',
    '@',
    '#',
    '$',
    '%',
    '^',
    '&',
    '*',
    '(',
    ')',
    '-',
    '_',
    '+',
    '=',
    '[',
    ']',
    ';',
    ':',
    '"',
    '\'',
    ',',
    '<',
    '.',
    '>',
    '?',
    '/',
    '\\',
    '|',
  };
}
