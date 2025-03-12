using System.Threading;
using System.Threading.Tasks;
using Ecommerce.Application.Common.Errors;
using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.Common.Utilities;
using Ecommerce.Domain.Common.ValueObjects;
using Ecommerce.Domain.UserAggregate.ValueObjects;
using FluentResults;
using MediatR;

namespace Ecommerce.Application.UseCases.Users.Commands.VerifyAccount
{
  public class VerifyAccountCommandHandler : IRequestHandler<VerifyAccountCommand, Result>
  {
    private readonly IUserRepository _userRepository;

    public VerifyAccountCommandHandler(IUserRepository userRepository)
    {
      _userRepository = userRepository;
    }

    public async Task<Result> Handle(
      VerifyAccountCommand request,
      CancellationToken cancellationToken
    )
    {
      var userId = UserId.Convert(ConversionUtility.ToGuid(request.userId).Value);
      var user = await _userRepository.GetByIdAsync(userId);

      if (user is null)
      {
        return NotFoundError.GetResult(nameof(request.userId), "User not found.");
      }

      var verifyResult = user.VerifyEmail(
        OneTimePassword.Convert(ConversionUtility.ToIntArray(request.otp).Value).Value
      );

      if (verifyResult.IsFailed)
        return verifyResult;

      user.SetEmailVerificationOtp(null); // clear the OTP after verification
      user.AccountVerified = true;
      _userRepository.Update(user);

      return Result.Ok();
    }
  }
}
