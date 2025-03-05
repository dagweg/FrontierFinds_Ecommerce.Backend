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
using Microsoft.AspNetCore.Identity;

public class RegisterUserCommandHandler
  : IRequestHandler<RegisterUserCommand, Result<AuthenticationResult>>
{
    // Repository Injection
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IAuthenticationMessages _authValidationMessages;
    private readonly IUserValidationService _userValidationService;
    private readonly IPasswordHasher<User> _passwordHasher;

    public RegisterUserCommandHandler(
      IUserRepository userRespository,
      IJwtTokenGenerator jwtTokenGenerator,
      IAuthenticationMessages authValidationMessages,
      IUserValidationService userValidationService,
      IUnitOfWork unitOfWork,
      IPasswordHasher<User> passwordHasher
    )
    {
        _userRepository = userRespository;
        _jwtTokenGenerator = jwtTokenGenerator;
        _authValidationMessages = authValidationMessages;
        _userValidationService = userValidationService;
        _unitOfWork = unitOfWork;
        _passwordHasher = passwordHasher;
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
          email: email,
          password: Password.CreateRandom(),
          phoneNumber: PhoneNumber.Create(command.PhoneNumber),
          countryCode: command.CountryCode
        );

        // hash the password using the user object as salt
        var hashedPassword = _passwordHasher.HashPassword(user, command.Password);

        var passwordResult = Password.Create(hashedPassword, command.Password);

        if (passwordResult.IsFailed)
            return passwordResult.ToResult();

        // make the hashed password the new password
        user.ChangePassword(passwordResult.Value);

        // 3. Make Changes to User Repository
        await _userRepository.AddAsync(user);

        // 4. Persist the Changes
        await _unitOfWork.SaveChangesAsync();

        // 4. Generate the Token
        var tokenResult = _jwtTokenGenerator.GenerateToken(
          user.Id,
          user.Email,
          user.FirstName,
          user.LastName
        );

        if (tokenResult.IsFailed)
            return tokenResult.ToResult();

        // 5. Return the Authentication Result
        return new AuthenticationResult()
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.FirstName,
            Token = tokenResult.Value,
        };
    }
}
