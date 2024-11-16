using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthenticationController : ControllerBase
{
    [HttpPost("register")]
    public Task<IActionResult> Register()
    {
        return Task.FromResult<IActionResult>(Ok("Registration successful"));
    }

    [HttpPost("login")]
    public Task<IActionResult> Login()
    {
        return Task.FromResult<IActionResult>(Ok("Login successful"));
    }
}