using Ecommerce.Application.Common.Errors;
using Ecommerce.Application.Common.Extensions;
using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.Common.Interfaces.Providers.Context;
using Ecommerce.Application.Common.Utilities;
using Ecommerce.Application.UseCases.Stmp.Commands.SendEmail;
using Ecommerce.Domain.Common.ValueObjects;
using Ecommerce.Domain.UserAggregate;
using Ecommerce.Domain.UserAggregate.ValueObjects;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Application.UseCases.Users.Commands.ResetPasswordVerify;

public class ResetPasswordVerifyCommandHandler : IRequestHandler<ResetPasswordVerifyCommand, Result>
{
  private readonly IUserContextService _userContext;
  private readonly IUserRepository _userRepository;
  private readonly IPasswordHasher<User> _passwordHasher;
  private readonly IUnitOfWork _unitOfWork;
  private readonly IMediator _sender;

  public ResetPasswordVerifyCommandHandler(
    IUserContextService userContext,
    IUserRepository userRepository,
    IPasswordHasher<User> passwordHasher,
    IUnitOfWork unitOfWork,
    IMediator sender
  )
  {
    _userContext = userContext;
    _userRepository = userRepository;
    _passwordHasher = passwordHasher;
    _unitOfWork = unitOfWork;
    _sender = sender;
  }

  public async Task<Result> Handle(
    ResetPasswordVerifyCommand request,
    CancellationToken cancellationToken
  )
  {
    var userIdResult = _userContext.GetValidUserId();
    if (userIdResult.IsFailed)
      return userIdResult.ToResult();

    var userId = UserId.Convert(userIdResult.Value);
    var user = await _userRepository.GetByIdAsync(userId);
    if (user is null)
    {
      return NotFoundError.GetResult(nameof(User), "User not found");
    }

    // convert OTP string to int array
    var intArrResult = ConversionUtility.ToIntArray(request.Otp);

    if (intArrResult.IsFailed)
      return intArrResult.ToResult();

    // convert int array to OneTimePassword
    var otpResult = OneTimePassword.Convert(intArrResult.Value);

    if (otpResult.IsFailed)
      return otpResult.ToResult();

    // Verify the corretness of the OneTimePassword
    var verifyOtpResult = user.VerifyPasswordResetOtp(otpResult.Value);

    if (verifyOtpResult.IsFailed)
      return verifyOtpResult;

    // If the OTP is correct, reset the otp and the password
    user.SetPasswordResetOtp(null);

    var passwordResult = Password.Create(
      _passwordHasher.HashPassword(user, request.NewPassword),
      request.NewPassword
    );

    if (passwordResult.IsFailed)
      return passwordResult.ToResult();

    user.ChangePassword(passwordResult.Value);

    _userRepository.Update(user);

    await _unitOfWork.SaveChangesAsync(cancellationToken);

    return Result.Ok();
  }
}
