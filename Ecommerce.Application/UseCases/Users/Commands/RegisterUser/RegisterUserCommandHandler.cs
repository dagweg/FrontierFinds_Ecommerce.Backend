namespace Ecommerce.Application.UseCases.Users.Commands.RegisterUser;

using Ecommerce.Application.Common.Errors;
using Ecommerce.Application.Common.Interfaces.Authentication;
using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.UseCases.Users.Common;
using Ecommerce.Domain.Common.ValueObjects;
using Ecommerce.Domain.UserAggregate;
using FluentResults;
using MediatR;

public class RegisterUserCommandHandler
  : IRequestHandler<RegisterUserCommand, Result<AuthenticationResult>>
{
  // Repository Injection
  private readonly IUserRepository _userRepository;
  private readonly IJwtTokenGenerator _jwtTokenGenerator;

  public RegisterUserCommandHandler(
    IUserRepository userRespository,
    IJwtTokenGenerator jwtTokenGenerator
  )
  {
    _userRepository = userRespository;
    _jwtTokenGenerator = jwtTokenGenerator;
  }

  public async Task<Result<AuthenticationResult>> Handle(
    RegisterUserCommand command,
    CancellationToken cancellationToken
  )
  {
    User? user = await _userRepository.GetByEmailAsync(Email.Create(command.Email));
    // 1. Check if the User Exists
    if (user is not null)
      return Result.Fail(EmailAlreadyExistsException.DefaultMessage);

    // 2. Create the User
    user = User.Create(
      Name.Create(command.FirstName),
      Name.Create(command.LastName),
      Email.Create(command.Email),
      Password.Create(command.Password),
      PhoneNumber.Create(command.Password)
    );

    // 3. Persist to the Database
    await _userRepository.AddAsync(user);

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
