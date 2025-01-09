namespace Ecommerce.Application.UseCases.Users.Commands.RegisterUser;

using Ecommerce.Application.Common.Errors;
using Ecommerce.Application.Common.Interfaces.Authentication;
using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.Common.Interfaces.Providers.Localization;
using Ecommerce.Application.Common.Interfaces.Validation;
using Ecommerce.Application.UseCases.Users.Common;
using Ecommerce.Domain.Common.ValueObjects;
using Ecommerce.Domain.UserAggregate;
using Ecommerce.Domain.UserAggregate.Entities;
using Ecommerce.Domain.UserAggregate.ValueObjects;
using FluentResults;
using MediatR;

public class RegisterUserCommandHandler
  : IRequestHandler<RegisterUserCommand, Result<AuthenticationResult>>
{
  // Repository Injection
  private readonly IUserRepository _userRepository;
  private readonly IJwtTokenGenerator _jwtTokenGenerator;
  private readonly IAuthenticationMessages _authValidationMessages;
  private readonly IUserValidationService _userValidationService;

  public RegisterUserCommandHandler(
    IUserRepository userRespository,
    IJwtTokenGenerator jwtTokenGenerator,
    IAuthenticationMessages authValidationMessages,
    IUserValidationService userValidationService
  )
  {
    _userRepository = userRespository;
    _jwtTokenGenerator = jwtTokenGenerator;
    _authValidationMessages = authValidationMessages;
    _userValidationService = userValidationService;
  }

  public async Task<Result<AuthenticationResult>> Handle(
    RegisterUserCommand command,
    CancellationToken cancellationToken
  )
  {
    var email = Email.Create(command.Email);
    var emailResult = await _userValidationService.CheckIfUserAlreadyExistsAsync(email);
    if (emailResult.IsFailed)
      return emailResult;

    var phoneNumber = PhoneNumber.Create(command.PhoneNumber);
    var phoneResult = await _userValidationService.CheckIfUserAlreadyExistsAsync(phoneNumber);
    if (phoneResult.IsFailed)
      return phoneResult;

    // 2. Create the User
    var user = User.Create(
      firstName: Name.Create(command.FirstName),
      lastName: Name.Create(command.LastName),
      email: Email.Create(command.Email),
      password: Password.Create(command.Password),
      phoneNumber: PhoneNumber.Create(command.Password),
      countryCode: command.CountryCode
    );

    // 3. Persist to the Database
    await _userRepository.AddAsync(user);

    await _userRepository.SaveChangesAsync();

    // 4. Generate the Token
    string token = _jwtTokenGenerator.GenerateToken(
      user.Id,
      user.Email,
      user.FirstName,
      user.LastName
    );

    // 5. Return the Authentication Result
    return new AuthenticationResult()
    {
      Id = user.Id,
      Email = user.Email,
      FirstName = user.FirstName,
      LastName = user.FirstName,
      Token = token,
    };
  }
}
