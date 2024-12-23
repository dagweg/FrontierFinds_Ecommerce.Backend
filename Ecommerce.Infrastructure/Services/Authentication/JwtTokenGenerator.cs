using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Unicode;
using Ecommerce.Application.Common.Interfaces.Authentication;
using Ecommerce.Application.UseCases.Users.Common;
using Ecommerce.Domain.Common.ValueObjects;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Ecommerce.Infrastructure.Services.Authentication;

public class JwtTokenGenerator : IJwtTokenGenerator
{
  private readonly JwtSettings _jwtSettings;

  public JwtTokenGenerator(IOptions<JwtSettings> jwtSettings)
  {
    _jwtSettings = jwtSettings.Value;
  }

  public string GenerateToken(UserId userId, Email email, Name firstName, Name lastName)
  {
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
      expires: DateTime.Now.AddMinutes(_jwtSettings.ExpiryMinutes)
    );

    // Write the JwtSecurityToken into a String (Serialized form)
    return new JwtSecurityTokenHandler().WriteToken(token);
  }
}
