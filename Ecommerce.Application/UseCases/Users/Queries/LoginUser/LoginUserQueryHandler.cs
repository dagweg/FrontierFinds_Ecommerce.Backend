using Ecommerce.Application.Common.Errors;
using Ecommerce.Application.Common.Interfaces.Authentication;
using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.UseCases.Users.Common;
using Ecommerce.Domain.Common.ValueObjects;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Application.UseCases.Users.Queries.LoginUser;

public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, Result<AuthenticationResult>>
{
  private readonly IUserRepository _userRepository;
  private readonly ILogger<LoginUserQueryHandler> _logger;
  private readonly IJwtTokenGenerator _jwtTokenGenerator;

  public LoginUserQueryHandler(
    IUserRepository userRespository,
    IJwtTokenGenerator jwtTokenGenerator,
    ILogger<LoginUserQueryHandler> logger
  )
  {
    _userRepository = userRespository;
    _logger = logger;
    _jwtTokenGenerator = jwtTokenGenerator;
  }

  public async Task<Result<AuthenticationResult>> Handle(
    LoginUserQuery query,
    CancellationToken cancellationToken
  )
  {
    try
    {
      // 1. Check if the user with the given email exists
      var user = await _userRepository.GetByEmailAsync(Email.Create(query.Email));

      if (user is null)
        return Result.Fail(UserNotFoundException.DefaultMessage);

      // 2. Check if the password is correct
      // Note: use bcrypt hash for comparing the password in production,
      // it's okay to compare literal passwords in dev envrionment
      if (!user.Password.Value.Equals(query.Password))
        return Result.Fail(PasswordIncorrectException.DefaultMessage);

      // 3. Generate a JWT token
      var token = _jwtTokenGenerator.GenerateToken(
        user.Id,
        user.Email,
        user.FirstName,
        user.LastName
      );

      // 4. Return the user's information and the token
      var result = new AuthenticationResult
      {
        Id = user.Id,
        FirstName = user.FirstName,
        Email = user.Email,
        LastName = user.LastName,
        Token = token,
      };
      return Result.Ok(result);
    }
    catch (Exception ex)
    {
      _logger.LogError(
        "An error occurred while logging in the User with Email: {@Email}. Exception: {@ExceptionMessage} {@StackTrace}",
        query.Email,
        ex.Message,
        ex.StackTrace
      );

      return Result.Fail("An error occurred while logging in the user");
    }
  }
}
