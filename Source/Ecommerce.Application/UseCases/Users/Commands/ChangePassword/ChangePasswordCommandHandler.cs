using Ecommerce.Application.Common.Errors;
using Ecommerce.Application.Common.Extensions;
using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.Common.Interfaces.Providers.Context;
using Ecommerce.Domain.Common.ValueObjects;
using Ecommerce.Domain.UserAggregate;
using Ecommerce.Domain.UserAggregate.ValueObjects;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.Application.UseCases.Users.Commands.ChangePassword;

public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result>
{
    private readonly IUserContextService _userContext;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IUnitOfWork _unitOfWork;

    public ChangePasswordCommandHandler(
      IUserContextService userContext,
      IUserRepository userRepository,
      IPasswordHasher<User> passwordHasher,
      IUnitOfWork unitOfWork
    )
    {
        _userContext = userContext;
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(
      ChangePasswordCommand request,
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

        // make sure the new password is different from the current password
        if (request.CurrentPassword == request.NewPassword)
        {
            return PasswordMatchError.GetResult(
              nameof(ChangePasswordCommand.NewPassword),
              "New password must be different from the current password"
            );
        }

        // validate hashed password
        if (
          _passwordHasher.VerifyHashedPassword(user, user.Password, request.CurrentPassword)
          == PasswordVerificationResult.Failed
        )
        {
            return IncorrectCurrentPasswordError.GetResult(
              nameof(ChangePasswordCommand.CurrentPassword),
              "Current password is incorrect"
            );
        }

        // change the password since the current password is correct.
        var newPasswordResult = Password.Create(
          _passwordHasher.HashPassword(user, request.NewPassword),
          request.NewPassword
        );

        if (newPasswordResult.IsFailed)
            return newPasswordResult.ToResult();

        user.ChangePassword(newPasswordResult.Value);

        _userRepository.Update(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();
    }
}
