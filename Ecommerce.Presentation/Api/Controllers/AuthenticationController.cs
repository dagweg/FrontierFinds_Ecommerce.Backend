namespace Ecommerce.Presentation.Api.Controllers;

using AutoMapper;
using Contracts.Authentication;
using Ecommerce.Application.UseCases.Users.Commands.RegisterUser;
using Ecommerce.Application.UseCases.Users.Queries.LoginUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("auth")]
public class AuthenticationController : ControllerBase
{
  private readonly ISender _mediator;
  private readonly IMapper _mapper;

  public AuthenticationController(ISender mediator, IMapper mapper)
  {
    _mediator = mediator;
    _mapper = mapper;
  }

  [HttpPost("register")]
  public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
  {
    var command = _mapper.Map<RegisterUserCommand>(registerRequest);

    var result = await _mediator.Send(command);

    return result.IsFailed
      ? new ObjectResult(result)
      : Ok(_mapper.Map<AuthenticationResponse>(result.Value));
  }

  [HttpPost("login")]
  public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
  {
    var loginQuery = _mapper.Map<LoginUserQuery>(loginRequest);

    var result = await _mediator.Send(loginQuery);

    return result.IsFailed
      ? new ObjectResult(result)
      : Ok(_mapper.Map<AuthenticationResponse>(result.Value));
  }
}
