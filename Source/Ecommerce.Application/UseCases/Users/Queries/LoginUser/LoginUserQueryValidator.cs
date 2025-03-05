using Ecommerce.Application.Common;
using Ecommerce.Application.Common.Interfaces.Providers.Localization;
using FluentValidation;

namespace Ecommerce.Application.UseCases.Users.Queries.LoginUser;

/// <summary>
/// Validation class for Login Query
/// </summary>
public class LoginUserQueryValidator : AbstractValidator<LoginUserQuery>
{
    private readonly IValidationMessages _messages;

    public LoginUserQueryValidator(IValidationMessages messages)
    {
        _messages = messages;

        RuleFor(x => x.Email)
          .NotEmpty()
          .WithMessage(_messages.EmailRequired)
          .EmailAddress()
          .WithMessage(_messages.EmailInvalidFormat);

        RuleFor(x => x.Password).NotEmpty().WithMessage(_messages.PasswordRequired);
    }
}
