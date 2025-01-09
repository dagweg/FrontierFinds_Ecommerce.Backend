using Ecommerce.Application.Common;
using Ecommerce.Application.Common.Interfaces.Providers.Localization;
using FluentValidation;

namespace Ecommerce.Application.UseCases.Users.Commands.RegisterUser;

public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
  private readonly IValidationMessages _messages;

  public RegisterUserCommandValidator(IValidationMessages messages)
  {
    _messages = messages;

    RuleFor(x => x.FirstName).NotEmpty().WithMessage(_messages.FirstNameRequired);

    RuleFor(x => x.LastName).NotEmpty().WithMessage(_messages.LastNameRequired);

    RuleFor(x => x.Email)
      .NotEmpty()
      .WithMessage(_messages.EmailRequired)
      .EmailAddress()
      .WithMessage(_messages.EmailInvalidFormat);

    RuleFor(x => x.Password).NotEmpty().WithMessage(_messages.PasswordRequired);

    RuleFor(x => x.ConfirmPassword)
      .NotEmpty()
      .WithMessage(_messages.ConfirmPasswordRequired)
      .Equal(y => y.Password)
      .WithMessage(_messages.PasswordsDoNotMatch);

    RuleFor(x => x.CountryCode).NotEmpty().WithMessage(_messages.CountryCodeRequired);

    RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage(_messages.PhoneRequired);
  }
}
