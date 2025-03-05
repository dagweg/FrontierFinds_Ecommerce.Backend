using AutoMapper;
using Ecommerce.Application.Common.Errors;
using Ecommerce.Application.Common.Extensions;
using Ecommerce.Application.Common.Interfaces.Authentication;
using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.Common.Interfaces.Providers.Localization;
using Ecommerce.Application.UseCases.Users.Common;
using Ecommerce.Domain.Common.ValueObjects;
using Ecommerce.Domain.UserAggregate;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Application.UseCases.Users.Queries.LoginUser;

public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, Result<AuthenticationResult>>
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<LoginUserQueryHandler> _logger;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IMapper _mapper;
    private readonly IAuthenticationMessages _authValidationMessages;
    private readonly IPasswordHasher<User> _passwordHasher;

    public LoginUserQueryHandler(
      IUserRepository userRespository,
      IJwtTokenGenerator jwtTokenGenerator,
      ILogger<LoginUserQueryHandler> logger,
      IMapper mapper,
      IAuthenticationMessages authValidationMessages,
      IPasswordHasher<User> passwordHasher
    )
    {
        _userRepository = userRespository;
        _logger = logger;
        _jwtTokenGenerator = jwtTokenGenerator;
        _mapper = mapper;
        _authValidationMessages = authValidationMessages;
        _passwordHasher = passwordHasher;
    }

    public async Task<Result<AuthenticationResult>> Handle(
      LoginUserQuery query,
      CancellationToken cancellationToken
    )
    {
        // 1. Check if the user with the given email exists
        var user = await _userRepository.GetByEmailAsync(Email.Create(query.Email));

        if (user is null)
            return Result.Fail(
              new AuthenticationError(nameof(Email), _authValidationMessages.UserNotFound)
            );

        // 2. Check if the password is correct
        if (
          _passwordHasher.VerifyHashedPassword(user, user.Password.ValueHash, query.Password)
          == PasswordVerificationResult.Failed
        )
        {
            return Result.Fail(
              new AuthenticationError(
                nameof(query.Password),
                _authValidationMessages.EmailOrPasswordIncorrect
              )
            );
        }

        // 3. Generate a JWT token
        var tokenResult = _jwtTokenGenerator.GenerateToken(
          user.Id,
          user.Email,
          user.FirstName,
          user.LastName
        );

        if (tokenResult.IsFailed)
            return tokenResult.ToResult();

        // 4. Return the user's information and the token
        var result = _mapper.Map<AuthenticationResult>(user);
        result.Token = tokenResult.Value;

        return Result.Ok(result);
    }
}
