namespace Ecommerce.Presentation.Api.Controllers;

using Contracts.Authentication;
using Ecommerce.Application.UseCases.Users.Commands.RegisterUser;
using Ecommerce.Application.UseCases.Users.Queries.LoginUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("auth")]
public class AuthenticationController(IMediator mediator) : ControllerBase
{
  [HttpPost("register")]
  public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
  {
    var command = new RegisterUserCommand(
      registerRequest.FirstName,
      registerRequest.LastName,
      registerRequest.Email,
      registerRequest.Password,
      registerRequest.PhoneNumber,
      registerRequest.CountryCode
    );

    var result = await mediator.Send(command);

    return Ok(result);
  }

  [HttpPost("login")]
  public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
  {
    var loginQuery = new LoginUserQuery(loginRequest.Email, loginRequest.Password);

    var result = await mediator.Send(loginQuery);

    return Ok(result);
  }
}
