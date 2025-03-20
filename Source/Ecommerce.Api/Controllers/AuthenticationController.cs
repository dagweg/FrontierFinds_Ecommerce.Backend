namespace Ecommerce.Api.Controllers;

using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Ecommerce.Application.Common.Utilities;
using Ecommerce.Application.UseCases.Users.Commands.RegisterUser;
using Ecommerce.Application.UseCases.Users.Commands.ResendEmailOtp;
using Ecommerce.Application.UseCases.Users.Commands.VerifyAccount;
using Ecommerce.Application.UseCases.Users.Queries.LoginUser;
using Ecommerce.Contracts.Authentication;
using Ecommerce.Infrastructure.Services.Authentication;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

[ApiController]
[Route("auth")]
public class AuthenticationController : ControllerBase
{
  private readonly ISender _mediator;
  private readonly IMapper _mapper;
  private readonly CookieSettings _cookieSettings;
  private readonly IWebHostEnvironment _webHostEnv;
  private readonly ILogger<AuthenticationController> _logger;

  public AuthenticationController(
    ISender mediator,
    IMapper mapper,
    IOptions<CookieSettings> cookieSettings,
    IWebHostEnvironment webHostEnv,
    ILogger<AuthenticationController> logger
  )
  {
    _mediator = mediator;
    _mapper = mapper;
    _cookieSettings = cookieSettings.Value;
    _webHostEnv = webHostEnv;
    _logger = logger;
  }

  [HttpPost("register")]
  public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
  {
    var command = _mapper.Map<RegisterUserCommand>(registerRequest);

    var result = await _mediator.Send(command);

    return result.IsFailed ? new ObjectResult(result) : Ok(result.Value);
  }

  [HttpPost("login")]
  public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
  {
    var loginQuery = _mapper.Map<LoginUserQuery>(loginRequest);

    var result = await _mediator.Send(loginQuery);

    if (result.IsFailed)
      return new ObjectResult(result);

    var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(result.Value.Token);
    var expClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == JwtRegisteredClaimNames.Exp);

    if (expClaim is null)
    {
      _logger.LogError(
        "Token does not contain an expiration claim. Token: {Token}",
        result.Value.Token
      );
      return StatusCode(500, "An error occured trying to log you in. Please contact support.");
    }

    _logger.LogInformation($"CookieSettings.Secure value: {_cookieSettings.Secure}"); // **ADD THIS LINE**

    // Set the http-only cookie
    HttpContext.Response.Cookies.Append(
      _cookieSettings.CookieKey,
      result.Value.Token,
      new CookieOptions
      {
        HttpOnly = _cookieSettings.HttpOnly,
        // Secure = _cookieSettings.Secure,
        Secure = true,
        Expires = DateTimeOffset.FromUnixTimeSeconds(long.Parse(expClaim.Value)),
        // SameSite = _cookieSettings.SameSite,
        SameSite = SameSiteMode.None,
        Path = "/",
      }
    );

    return Ok(_mapper.Map<AuthenticationResponse>(result.Value));
  }

  [HttpPost("verify")]
  public async Task<IActionResult> VerifyAccount([FromBody] VerifyAccountRequest verifyRequest)
  {
    var result = await _mediator.Send(
      new VerifyAccountCommand(verifyRequest.UserId, verifyRequest.Otp)
    );

    return result.IsFailed ? new ObjectResult(result) : Ok(new { });
  }

  [HttpPost("verify/resendOtp")]
  public async Task<IActionResult> ResendOtp([FromBody] ResendEmailOtpRequest resendRequest)
  {
    var result = await _mediator.Send(new ResendEmailOtpCommand(resendRequest.UserId));

    return result.IsFailed ? new ObjectResult(result) : Ok(result.Value);
  }

  [Authorize]
  [HttpGet("authorize")]
  public IActionResult Authorize()
  {
    return Ok(new { });
  }

  [Authorize]
  [HttpPost("logout")]
  public IActionResult Logout()
  {
    HttpContext.Response.Cookies.Delete(_cookieSettings.CookieKey);

    return Ok(new { });
  }
}
