using FluentResults;
using FluentValidation;
using MediatR;

namespace Ecommerce.Application.UseCases.Users.Commands.ResetPasswordVerify;

public class ResetPasswordVerifyCommandValidator : AbstractValidator<ResetPasswordVerifyCommand>
{
  public ResetPasswordVerifyCommandValidator()
  {
    RuleFor(x => x.NewPassword)
      .Equal(x => x.ConfirmNewPassword)
      .WithMessage("New password and confirm password must be the same");
  }
}
