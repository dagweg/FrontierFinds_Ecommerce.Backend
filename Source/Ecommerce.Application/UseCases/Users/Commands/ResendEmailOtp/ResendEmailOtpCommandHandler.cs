using System.Threading;
using System.Threading.Tasks;
using Ecommerce.Application.Common.Errors;
using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.Common.Utilities;
using Ecommerce.Domain.Common.Errors;
using Ecommerce.Domain.Common.ValueObjects;
using Ecommerce.Domain.UserAggregate.ValueObjects;
using FluentResults;
using MediatR;

namespace Ecommerce.Application.UseCases.Users.Commands.ResendEmailOtp
{
  public class ResendEmailOtpCommandHandler(
    IUserRepository userRepository,
    ISender sender,
    IUnitOfWork unitOfWork
  ) : IRequestHandler<ResendEmailOtpCommand, Result<ResendEmailOtpResult>>
  {
    async Task<Result<ResendEmailOtpResult>> IRequestHandler<
      ResendEmailOtpCommand,
      Result<ResendEmailOtpResult>
    >.Handle(ResendEmailOtpCommand request, CancellationToken cancellationToken)
    {
      var userId = UserId.Convert(ConversionUtility.ToGuid(request.userId).Value);
      var user = await userRepository.GetByIdAsync(userId);

      if (user is null)
        return NotFoundError.GetResult("userId", "User not found");

      if (user.EmailVerificationOtp is null)
        user.SetEmailVerificationOtp(OneTimePassword.CreateNew().Value);

      int waitSeconds = Math.Clamp(
        (int)(user.EmailVerificationOtp!.NextResendValidAt - DateTime.UtcNow).TotalSeconds,
        0,
        int.MaxValue
      );

      // Check if the time to resend the otp has reached
      if (DateTime.UtcNow < user.EmailVerificationOtp?.NextResendValidAt)
      {
        return new ResendEmailOtpResult { WaitSeconds = waitSeconds, IsSuccess = false };
      }

      // issues a new otp and adds delay on resend request
      user.SetEmailVerificationOtp(OneTimePassword.CreateNew().Value);

      // Send otp email
      await sender.Send(
        EmailDefaults.AccountVerificationEmail(
          user.Email.Value,
          user.EmailVerificationOtp!.ToString()
        )
      );

      userRepository.Update(user);

      await unitOfWork.SaveChangesAsync();

      return new ResendEmailOtpResult { WaitSeconds = waitSeconds, IsSuccess = true };
    }
  }
}
