using Ecommerce.Application.Common.Errors;
using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.Common.Interfaces.Providers.Localization;
using Ecommerce.Application.Common.Interfaces.Validation;
using Ecommerce.Application.UseCases.Users.Common;
using Ecommerce.Domain.Common.ValueObjects;
using Ecommerce.Domain.UserAggregate;
using FluentResults;

namespace Ecommerce.Application.Services.Validation;

public class UserValidationService : IUserValidationService
{
  private readonly IUserRepository _userRepository;
  private readonly IAuthenticationMessages _authValidationMessages;

  public UserValidationService(
    IUserRepository userRepository,
    IAuthenticationMessages authValidationMessages
  )
  {
    _userRepository = userRepository;
    _authValidationMessages = authValidationMessages;
  }

  public async Task<Result> CheckIfUserAlreadyExistsAsync(Email email)
  {
    User? user = await _userRepository.GetByEmailAsync(email);

    // 1. Check if the User Exists
    // if so return an error
    if (user is not null)
    {
      return AlreadyExistsError.GetResult(nameof(email), _authValidationMessages.UserAlreadyExists);
    }

    return Result.Ok();
  }

  public async Task<Result> CheckIfUserAlreadyExistsAsync(PhoneNumber phoneNumber)
  {
    User? user = await _userRepository.GetByPhoneNumberAsync(phoneNumber);

    if (user is not null)
    {
      return AlreadyExistsError.GetResult(
        nameof(phoneNumber),
        _authValidationMessages.UserAlreadyExists
      );
    }

    return Result.Ok();
  }
}
