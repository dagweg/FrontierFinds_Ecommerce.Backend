using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Ecommerce.Application.Common.Interfaces.Authentication;
using Ecommerce.Application.Common.Interfaces.Providers.Date;
using Ecommerce.Application.Common.Interfaces.Providers.Localization;
using Ecommerce.Application.Common.Utilities;
using Ecommerce.Domain.Common.ValueObjects;
using Ecommerce.Domain.UserAggregate.ValueObjects;
using FluentResults;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Ecommerce.Infrastructure.Services.Authentication;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly JwtSettings _jwtSettings;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly ILogger<JwtTokenGenerator> _logger;

    public JwtTokenGenerator(
      IOptions<JwtSettings> jwtSettings,
      IDateTimeProvider dateTimeProvider,
      ILogger<JwtTokenGenerator> logger
    )
    {
        _jwtSettings = jwtSettings.Value;
        _dateTimeProvider = dateTimeProvider;
        _logger = logger;
    }

    /// <summary>
    /// Generates a JWT token for the given user
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="email"></param>
    /// <param name="firstName"></param>
    /// <param name="lastName"></param>
    /// <returns>
    ///   A JWT token string, or null if the token could not be generated.
    /// </returns>
    /// <exception cref="InvalidOperationException"></exception>
    public Result<string> GenerateToken(UserId userId, Email email, Name firstName, Name lastName)
    {
        if (_jwtSettings.SecretKey is null)
        {
            _logger.LogError("Jwt secret key is null");
            return Result.Fail<string>("An internal error occurred. Please try again later.");
        }

        // Create a signing credential using HMACSHA256 algorithm and a Symmetric Security Key
        SigningCredentials signingCredential = new(
          new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey)),
          SecurityAlgorithms.HmacSha256
        );

        // Create the claims that will be included in the JWT
        var claims = new[]
        {
      new Claim(JwtRegisteredClaimNames.Sub, userId),
      new Claim(JwtRegisteredClaimNames.Iss, _jwtSettings.Issuer),
      new Claim(JwtRegisteredClaimNames.Aud, _jwtSettings.Audience),
      new Claim(JwtRegisteredClaimNames.GivenName, firstName),
      new Claim(JwtRegisteredClaimNames.FamilyName, lastName),
      new Claim(JwtRegisteredClaimNames.Email, email),
      new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
    };

        // Use the Claims and SigningCredentials to create the JWTSecurityToken
        JwtSecurityToken token = new(
          issuer: _jwtSettings.Issuer,
          claims: claims,
          signingCredentials: signingCredential,
          expires: _dateTimeProvider.UtcNow.AddMinutes(_jwtSettings.ExpiryMinutes)
        );

        // Write the JwtSecurityToken into a String (Serialized form)
        return Result.Ok(new JwtSecurityTokenHandler().WriteToken(token));
    }
}
