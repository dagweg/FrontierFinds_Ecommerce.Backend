using Ecommerce.Application.Common;
using Ecommerce.Application.Common.Interfaces;
using FluentValidation;

namespace Ecommerce.Application.UseCases.Users.Queries.LoginUser;

/// <summary>
/// Validation class for Login Query
/// </summary>
public class LoginUserQueryValidator : AbstractValidator<LoginUserQuery>
{
  private readonly IValidationMessageProvider _messages;

  public LoginUserQueryValidator(IValidationMessageProvider messages)
  {
    _messages = messages;

    RuleFor(x => x.Email)
      .NotEmpty()
      .WithMessage(_messages.GetMessage(ValidationMessageKeys.EmailRequired))
      .EmailAddress()
      .WithMessage(_messages.GetMessage(ValidationMessageKeys.EmailInvalidFormat));

    RuleFor(x => x.Password)
      .NotEmpty()
      .WithMessage(_messages.GetMessage(ValidationMessageKeys.PasswordRequired));
  }
}
