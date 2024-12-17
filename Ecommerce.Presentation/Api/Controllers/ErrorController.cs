namespace Ecommerce.Presentation.Api.Controllers;

using Microsoft.AspNetCore.Mvc;

[Route("error")]
public class ErrorController : ControllerBase
{
    [HttpGet]
    public Task<IActionResult> Error() => Task.FromResult<IActionResult>(Ok("An error occurred"));
}
