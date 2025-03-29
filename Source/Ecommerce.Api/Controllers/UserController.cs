namespace Ecommerce.Api.Controllers;

using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices;
using AutoMapper;
using Ecommerce.Api.Attributes;
using Ecommerce.Api.Utilities;
using Ecommerce.Application.Common.Models;
using Ecommerce.Application.UseCases.Products.Queries.GetAllProducts;
using Ecommerce.Application.UseCases.Products.Queries.GetFilteredProducts;
using Ecommerce.Application.UseCases.Users.Commands.AddToCart;
using Ecommerce.Application.UseCases.Users.Commands.ChangePassword;
using Ecommerce.Application.UseCases.Users.Commands.RegisterUser;
using Ecommerce.Application.UseCases.Users.Commands.RemoveFromCart;
using Ecommerce.Application.UseCases.Users.Commands.RemoveWishlistProducts;
using Ecommerce.Application.UseCases.Users.Commands.ResetPassword;
using Ecommerce.Application.UseCases.Users.Commands.ResetPasswordVerify;
using Ecommerce.Application.UseCases.Users.Commands.UpdateCart;
using Ecommerce.Application.UseCases.Users.Commands.WishlistProducts;
using Ecommerce.Application.UseCases.Users.Queries.GetAllUsers;
using Ecommerce.Application.UseCases.Users.Queries.GetCartItems;
using Ecommerce.Application.UseCases.Users.Queries.GetMyProducts;
using Ecommerce.Application.UseCases.Users.Queries.GetUser;
using Ecommerce.Application.UseCases.Users.Queries.LoginUser;
using Ecommerce.Contracts.Authentication;
using Ecommerce.Contracts.Cart;
using Ecommerce.Contracts.Common;
using Ecommerce.Contracts.Product;
using Ecommerce.Contracts.User;
using Ecommerce.Domain.UserAggregate.Entities;
using Ecommerce.Domain.UserAggregate.ValueObjects;
using Ecommerce.Infrastructure.Services.Authentication;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

[Authorize]
[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
  private readonly ISender _mediator;
  private readonly IMapper _mapper;
  private readonly CookieSettings _cookieSettings;
  private readonly IWebHostEnvironment _webHostEnv;
  private readonly ILogger<AuthenticationController> _logger;

  public UserController(
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

  [HttpGet("{id}")]
  public async Task<IActionResult> GetUserById([FromRoute] [Attributes.Guid] string id)
  {
    var res = await _mediator.Send(new GetUserQuery(id));

    if (res.IsFailed)
    {
      return new ObjectResult(res);
    }

    return Ok(res.Value);
  }

  [HttpGet]
  public async Task<IActionResult> GetAllUsers()
  {
    var res = await _mediator.Send(new GetAllUsersQuery());

    if (res.IsFailed)
    {
      return new ObjectResult(res);
    }

    return Ok(res.Value);
  }
}
