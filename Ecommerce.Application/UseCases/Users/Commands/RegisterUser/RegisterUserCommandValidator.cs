using Ecommerce.Application.Common;
using Ecommerce.Application.Common.Interfaces;
using FluentValidation;

namespace Ecommerce.Application.UseCases.Users.Commands.RegisterUser;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
  private readonly IValidationMessageProvider _messages;

  public RegisterUserCommandValidator(IValidationMessageProvider messages)
  {
    _messages = messages;

    RuleFor(x => x.FirstName)
      .NotEmpty()
      .WithMessage(_messages.GetMessage(ValidationMessageKeys.FirstNameRequired));

    RuleFor(x => x.LastName)
      .NotEmpty()
      .WithMessage(_messages.GetMessage(ValidationMessageKeys.LastNameRequired));

    RuleFor(x => x.Email)
      .NotEmpty()
      .WithMessage(_messages.GetMessage(ValidationMessageKeys.EmailRequired))
      .EmailAddress()
      .WithMessage(_messages.GetMessage(ValidationMessageKeys.EmailInvalidFormat));

    RuleFor(x => x.Password)
      .NotEmpty()
      .WithMessage(_messages.GetMessage(ValidationMessageKeys.PasswordRequired));

    RuleFor(x => x.ConfirmPassword)
      .NotEmpty()
      .WithMessage(_messages.GetMessage(ValidationMessageKeys.ConfirmPasswordRequired))
      .Equal(y => y.Password)
      .WithMessage(_messages.GetMessage(ValidationMessageKeys.PasswordsDoNotMatch));

    RuleFor(x => x.CountryCode)
      .NotEmpty()
      .WithMessage(_messages.GetMessage(ValidationMessageKeys.CountryCodeRequired));

    RuleFor(x => x.PhoneNumber)
      .NotEmpty()
      .WithMessage(_messages.GetMessage(ValidationMessageKeys.PhoneRequired));
  }
}
