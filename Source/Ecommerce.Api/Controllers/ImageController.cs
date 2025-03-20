using Ecommerce.Application.UseCases.Images.Queries.GetSupportedImageMimes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers;

[ApiController]
[Route("images")]
public class ImageContoller(ISender mediator) : ControllerBase
{
  [AllowAnonymous]
  [HttpGet("mimes/supported")]
  public async Task<IActionResult> GetSupportedImageMimes()
  {
    var result = await mediator.Send(new GetSupportedImageMimesQuery());
    return Ok(result.Value);
  }
}
