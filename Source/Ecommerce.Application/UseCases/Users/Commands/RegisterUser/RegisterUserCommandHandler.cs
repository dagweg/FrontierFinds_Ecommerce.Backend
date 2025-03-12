namespace Ecommerce.Application.UseCases.Users.Commands.RegisterUser;

using Ecommerce.Application.Common.Errors;
using Ecommerce.Application.Common.Interfaces.Authentication;
using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.Common.Interfaces.Providers.Localization;
using Ecommerce.Application.Common.Interfaces.Validation;
using Ecommerce.Application.Common.Utilities;
using Ecommerce.Application.UseCases.Stmp.Commands.SendEmail;
using Ecommerce.Application.UseCases.Users.Common;
using Ecommerce.Domain.Common.Errors;
using Ecommerce.Domain.Common.ValueObjects;
using Ecommerce.Domain.UserAggregate;
using Ecommerce.Domain.UserAggregate.Entities;
using Ecommerce.Domain.UserAggregate.ValueObjects;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

public class RegisterUserCommandHandler
  : IRequestHandler<RegisterUserCommand, Result<AuthenticationResult>>
{
  // Repository Injection
  private readonly ISender _sender;
  private readonly IUnitOfWork _unitOfWork;
  private readonly IUserRepository _userRepository;
  private readonly IJwtTokenGenerator _jwtTokenGenerator;
  private readonly IAuthenticationMessages _authValidationMessages;
  private readonly IUserValidationService _userValidationService;
  private readonly IPasswordHasher<User> _passwordHasher;
  private readonly ILogger<RegisterUserCommandHandler> _logger;

  public RegisterUserCommandHandler(
    ISender sender,
    IUserRepository userRespository,
    IJwtTokenGenerator jwtTokenGenerator,
    IAuthenticationMessages authValidationMessages,
    IUserValidationService userValidationService,
    IUnitOfWork unitOfWork,
    IPasswordHasher<User> passwordHasher,
    ILogger<RegisterUserCommandHandler> logger
  )
  {
    _sender = sender;
    _userRepository = userRespository;
    _jwtTokenGenerator = jwtTokenGenerator;
    _authValidationMessages = authValidationMessages;
    _userValidationService = userValidationService;
    _unitOfWork = unitOfWork;
    _passwordHasher = passwordHasher;
    _logger = logger;
  }

  public async Task<Result<AuthenticationResult>> Handle(
    RegisterUserCommand command,
    CancellationToken cancellationToken
  )
  {
    var email = Email.Create(command.Email);

    var user = await _userRepository.GetByEmailAsync(email);

    if (user != null && !user.AccountVerified)
    {
      _logger.LogInformation("User already exists but is not verified. Resending OTP.");

      // generate a new token
      var tokenR = _jwtTokenGenerator.GenerateToken(
        user.Id,
        user.Email,
        user.FirstName,
        user.LastName
      );

      if (tokenR.IsFailed)
        return tokenR.ToResult();

      return new AuthenticationResult
      {
        Email = user.Email,
        FirstName = user.FirstName,
        LastName = user.LastName,
        Id = user.Id,
        Token = tokenR.Value,
        AlreadyExistsButUnverified = true,
      };
    }

    if (user != null)
    {
      _logger.LogError("User already exists but is verified. Returning already exists.");
      return AlreadyExistsError.GetResult(nameof(email), _authValidationMessages.UserAlreadyExists);
    }

    var phoneNumber = PhoneNumber.Create(command.PhoneNumber);
    var phoneResult = await _userValidationService.CheckIfUserAlreadyExistsAsync(phoneNumber);
    if (phoneResult.IsFailed)
    {
      _logger.LogError("User already exists but is verified. Returning already exists.");
      return phoneResult;
    }

    // 2. Create the User
    var newUser = User.Create(
      firstName: Name.Create(command.FirstName),
      lastName: Name.Create(command.LastName),
      email: email,
      password: Password.CreateRandom(),
      phoneNumber: PhoneNumber.Create(command.PhoneNumber),
      countryCode: command.CountryCode
    );

    // hash the password using the user object as salt
    var hashedPassword = _passwordHasher.HashPassword(newUser, command.Password);

    var passwordResult = Password.Create(hashedPassword, command.Password);

    if (passwordResult.IsFailed)
      return passwordResult.ToResult();

    _logger.LogInformation("Password hashed successfully.");

    // make the hashed password the new password
    newUser.ChangePassword(passwordResult.Value);

    newUser.SetEmailVerificationOtp(OneTimePassword.CreateNew().Value);

    // 3. Make Changes to User Repository
    await _userRepository.AddAsync(newUser);

    // 4. Persist the Changes
    await _unitOfWork.SaveChangesAsync();

    _logger.LogInformation("User created successfully.");

    // Send otp email
    await _sender.Send(
      EmailDefaults.AccountVerificationEmail(
        newUser.Email.Value,
        newUser.EmailVerificationOtp!.ToString()
      )
    );

    _logger.LogInformation("Email sent successfully.");

    // 4. Generate the Token
    var tokenResult = _jwtTokenGenerator.GenerateToken(
      newUser.Id,
      newUser.Email,
      newUser.FirstName,
      newUser.LastName
    );

    _logger.LogInformation("Token generated successfully.");

    if (tokenResult.IsFailed)
      return tokenResult.ToResult();

    _logger.LogInformation("Sending authentication result.");

    // 5. Return the Authentication Result
    return new AuthenticationResult()
    {
      Id = newUser.Id,
      Email = newUser.Email,
      FirstName = newUser.FirstName,
      LastName = newUser.FirstName,
      Token = tokenResult.Value,
    };
  }
}
