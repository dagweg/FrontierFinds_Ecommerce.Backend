using System.Text.RegularExpressions;
using Ecommerce.Application.Common;
using Ecommerce.Application.Common.Interfaces.Providers.Localization;
using FluentValidation;
using PhoneNumbers;

namespace Ecommerce.Application.UseCases.Users.Commands.RegisterUser;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
  private readonly IValidationMessages _messages;
  private readonly PhoneNumberUtil _phoneNumberUtil; // Instance of PhoneNumberUtil

  public RegisterUserCommandValidator(IValidationMessages messages)
  {
    _messages = messages;
    _phoneNumberUtil = PhoneNumberUtil.GetInstance(); // Initialize PhoneNumberUtil

    RuleFor(x => x.FirstName).NotEmpty().WithMessage(_messages.FirstNameRequired);

    RuleFor(x => x.LastName).NotEmpty().WithMessage(_messages.LastNameRequired);

    RuleFor(x => x.Email)
      .NotEmpty()
      .WithMessage(_messages.EmailRequired)
      .EmailAddress()
      .WithMessage(_messages.EmailInvalidFormat);

    RuleFor(x => x.Password)
      .NotEmpty()
      .WithMessage(_messages.PasswordRequired)
      .SetValidator(new PasswordValidator());

    RuleFor(x => x.ConfirmPassword)
      .NotEmpty()
      .WithMessage(_messages.ConfirmPasswordRequired)
      .Equal(y => y.Password)
      .WithMessage(_messages.PasswordsDoNotMatch);

    RuleFor(x => x.PhoneNumber)
      .NotEmpty()
      .WithMessage(_messages.PhoneRequired)
      .Must(BeValidInternationalPhoneNumber) // Custom validation for international numbers
      .WithMessage("Invalid Phone number format."); // Assuming you have a message for invalid format
  }

  private bool BeValidInternationalPhoneNumber(string phoneNumber)
  {
    try
    {
      // Parse without a default region to allow international numbers with country codes
      PhoneNumber number = _phoneNumberUtil.Parse(phoneNumber, null); // Passing null for defaultRegion

      // Check if the number is valid for *any* region (international format)
      return _phoneNumberUtil.IsValidNumber(number);
    }
    catch (NumberParseException)
    {
      return false; // Parsing failed, not a valid phone number format
    }
  }
}
