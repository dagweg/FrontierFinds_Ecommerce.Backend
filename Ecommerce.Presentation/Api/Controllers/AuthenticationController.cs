namespace Ecommerce.Presentation.Api.Controllers;

using Contracts.Authentication;
using Ecommerce.Application.UseCases.UserManagement.Authentication.Commands;
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
    public Task<IActionResult> Login() => Task.FromResult<IActionResult>(Ok("Login successful"));
}
