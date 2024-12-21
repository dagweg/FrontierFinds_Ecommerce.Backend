using Ecommerce.Application.Common.Interfaces.Authentication;
using Ecommerce.Application.Common.Interfaces.Logging;
using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.UseCases.Users.Common;
using Ecommerce.Domain.Common.ValueObjects;
using FluentResults;
using MediatR;

namespace Ecommerce.Application.UseCases.Users.Queries.LoginUser;

public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, Result<AuthenticationResult>>
{
    private readonly IUserRespository _userRepository;
    private readonly ILogService _logger;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public LoginUserQueryHandler(
        IUserRespository userRespository,
        IJwtTokenGenerator jwtTokenGenerator,
        ILogService logger
    )
    {
        _userRepository = userRespository;
        _logger = logger;
        _jwtTokenGenerator = jwtTokenGenerator;
    }

    public async Task<Result<AuthenticationResult>> Handle(
        LoginUserQuery request,
        CancellationToken cancellationToken
    )
    {
        try
        {
            // 1. Check if the user with the given email exists
            Email email = new(request.Email);
            var user = await _userRepository.GetByEmailAsync(email);

            if (user is null)
                return Result.Fail<AuthenticationResult>("User not found");

            // 2. Check if the password is correct
            if (!user.Password.Equals(request.Password))
                return Result.Fail<AuthenticationResult>("Invalid password");

            // 3. Generate a JWT token
            var token = _jwtTokenGenerator.GenerateToken(user.Email, user.FirstName, user.LastName);

            // 4. Return the user's information and the token
            var result = new AuthenticationResult
            {
                FirstName = user.FirstName,
                Email = user.Email,
                LastName = user.LastName,
                Token = token,
            };

            return Result.Ok(result);
        }
        catch (System.Exception ex)
        {
            _logger.LogError(
                $"An error occurred while processing login for email {request.Email}",
                ex
            );
            return Result.Fail<AuthenticationResult>("An error occurred while logging in the user");
        }
    }
}
