using Ecommerce.Domain.Common.ValueObjects;
using FluentValidation;

public class PasswordValidator : AbstractValidator<string>
{
  public PasswordValidator()
  {
    RuleFor(password => password)
      .NotEmpty() // Ensures password is not null or empty after trimming
      .MinimumLength(Password.MIN_PASSWORD_LENGTH)
      .WithMessage($"Min password length is {Password.MIN_PASSWORD_LENGTH}")
      .Must(HaveMinimumLowercase)
      .WithMessage(
        $"Password should have at least {Password.MIN_LOWERCASE_CHARS} lowercase characters."
      )
      .Must(HaveMinimumUppercase)
      .WithMessage(
        $"Password should have at least {Password.MIN_UPPERCASE_CHARS} uppercase characters."
      )
      .Must(HaveMinimumSpecialCharacters)
      .WithMessage(
        $"Password should have at least {Password.MIN_SPECIAL_CHARS} special characters."
      )
      .Must(HaveMinimumNumericCharacters)
      .WithMessage($"Password should have at least {Password.MIN_NUMERIC} numeric characters.");
  }

  private bool HaveMinimumLowercase(string password)
  {
    if (string.IsNullOrEmpty(password))
      return true; // Let NotEmpty handle empty passwords
    return password.Count(char.IsLower) >= Password.MIN_LOWERCASE_CHARS;
  }

  private bool HaveMinimumUppercase(string password)
  {
    if (string.IsNullOrEmpty(password))
      return true;
    return password.Count(char.IsUpper) >= Password.MIN_UPPERCASE_CHARS;
  }

  private bool HaveMinimumSpecialCharacters(string password)
  {
    if (string.IsNullOrEmpty(password))
      return true;
    return password.Count(c => Password.SpecialChars.Contains(c)) >= Password.MIN_SPECIAL_CHARS;
  }

  private bool HaveMinimumNumericCharacters(string password)
  {
    if (string.IsNullOrEmpty(password))
      return true;
    return password.Count(char.IsDigit) >= Password.MIN_NUMERIC;
  }
}
