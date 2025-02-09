using FluentResults;
using MediatR;

namespace Ecommerce.Application.UseCases.Users.Commands.ResetPasswordVerify;

public class ResetPasswordVerifyCommand : IRequest<Result>
{
  public required string Otp { get; set; }
  public required string NewPassword { get; set; }
  public required string ConfirmNewPassword { get; set; }
}
