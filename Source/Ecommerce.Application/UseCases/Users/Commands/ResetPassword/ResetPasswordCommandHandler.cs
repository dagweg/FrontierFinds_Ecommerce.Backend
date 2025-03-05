using Ecommerce.Application.Common.Errors;
using Ecommerce.Application.Common.Extensions;
using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.Common.Interfaces.Providers.Context;
using Ecommerce.Application.UseCases.Stmp.Commands.SendEmail;
using Ecommerce.Domain.Common.ValueObjects;
using Ecommerce.Domain.UserAggregate;
using Ecommerce.Domain.UserAggregate.ValueObjects;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Application.UseCases.Users.Commands.ResetPassword;

public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Result>
{
  private readonly IUserContextService _userContext;
  private readonly IUserRepository _userRepository;
  private readonly IPasswordHasher<User> _passwordHasher;
  private readonly IUnitOfWork _unitOfWork;
  private readonly IMediator _sender;

  public ResetPasswordCommandHandler(
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
    ResetPasswordCommand request,
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

    // issue a new OTP
    user.SetPasswordResetOtp(OneTimePassword.CreateNew());

    var emailResult = await _sender.Send(
      new SendEmailCommand
      {
        To = user.Email.Value,
        Subject = "Reset your Password.",
        Body = $"Your OTP is {string.Join("", user.PasswordResetOtp!.Value)}.",
      }
    );

    if (emailResult.IsFailed)
    {
      return emailResult;
    }

    _userRepository.Update(user);

    await _unitOfWork.SaveChangesAsync(cancellationToken);

    return Result.Ok();
  }
}
