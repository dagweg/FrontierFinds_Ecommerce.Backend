namespace Ecommerce.Api.Controllers;

using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Ecommerce.Api.Utilities;
using Ecommerce.Application.Common.Models;
using Ecommerce.Application.Common.Utilities;
using Ecommerce.Application.UseCases.Products.Queries.GetAllProducts;
using Ecommerce.Application.UseCases.Products.Queries.GetFilteredProducts;
using Ecommerce.Application.UseCases.Users.Commands.AddToCart;
using Ecommerce.Application.UseCases.Users.Commands.ChangePassword;
using Ecommerce.Application.UseCases.Users.Commands.ClearCart;
using Ecommerce.Application.UseCases.Users.Commands.RegisterUser;
using Ecommerce.Application.UseCases.Users.Commands.RemoveFromCart;
using Ecommerce.Application.UseCases.Users.Commands.RemoveWishlistProducts;
using Ecommerce.Application.UseCases.Users.Commands.ResetPassword;
using Ecommerce.Application.UseCases.Users.Commands.ResetPasswordVerify;
using Ecommerce.Application.UseCases.Users.Commands.UpdateCart;
using Ecommerce.Application.UseCases.Users.Commands.WishlistProducts;
using Ecommerce.Application.UseCases.Users.Queries.GetCartItems;
using Ecommerce.Application.UseCases.Users.Queries.GetMyProducts;
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
[Route("me")]
public class MeController : ControllerBase
{
  private readonly ISender _mediator;
  private readonly IMapper _mapper;
  private readonly CookieSettings _cookieSettings;
  private readonly IWebHostEnvironment _webHostEnv;
  private readonly ILogger<AuthenticationController> _logger;

  public MeController(
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

  [HttpPost("cart")]
  public async Task<IActionResult> AddToCart(
    [FromBody, MinLength(1)] List<AddCartItemRequest> addCartItemRequest
  )
  {
    var result = await _mediator.Send(
      new AddToCartCommand()
      {
        createCartItemCommands = addCartItemRequest
          .Select(cir => new CreateCartItemCommand
          {
            ProductId = cir.ProductId,
            Quantity = cir.Quantity,
          })
          .ToList(),
      }
    );

    if (result.IsFailed)
    {
      _logger.LogError("Failed to add product to cart: {@result}", result);
      return new ObjectResult(result);
    }
    return Ok(result.Value);
  }

  [HttpDelete("cart/{cartItemId}")]
  public async Task<IActionResult> RemoveFromCart([FromRoute, MinLength(1)] string cartItemId)
  {
    var result = await _mediator.Send(new RemoveFromCartCommand() { CartItemId = cartItemId });

    if (result.IsFailed)
    {
      _logger.LogError("Failed to delete cart item: {@result}", result);
      return new ObjectResult(result);
    }
    return Ok(result.Value);
  }

  [HttpDelete("cart")]
  public async Task<IActionResult> ClearCart()
  {
    var result = await _mediator.Send(new ClearCartCommand());

    if (result.IsFailed)
    {
      _logger.LogError("Failed to clear cart: {@result}", result);
      return new ObjectResult(result);
    }
    return NoContent();
  }

  [HttpPatch("cart")]
  public async Task<IActionResult> UpdateCart(
    [FromBody] UpdateCartItemRequest updateCartItemRequest
  )
  {
    var result = await _mediator.Send(
      new UpdateCartCommand()
      {
        CartItem = new UpdateCartItemCommand
        {
          CartItemId = updateCartItemRequest.CartItemId,
          Quantity = updateCartItemRequest.Quantity,
          Seen = updateCartItemRequest.Seen,
        },
      }
    );

    if (result.IsFailed)
    {
      _logger.LogError("Failed to add product to cart: {@result}", result);
      return new ObjectResult(result);
    }

    LogPretty.Log(result.Value);
    return Ok(result.Value);
  }

  [HttpGet("cart")]
  public async Task<IActionResult> GetCartItems([FromQuery] PaginationParams paginationParams)
  {
    var result = await _mediator.Send(
      new GetCartQuery(paginationParams.PageNumber, paginationParams.PageSize)
    );

    if (result.IsFailed)
    {
      _logger.LogError("Failed to get cart items : {@result}", result);
      return new ObjectResult(result);
    }

    return Ok(result.Value);
  }

  [HttpPost("password/change")]
  public async Task<IActionResult> ChangePassword(
    [FromBody] ChangePasswordRequest changePasswordRequest
  )
  {
    var result = await _mediator.Send(
      new ChangePasswordCommand
      {
        CurrentPassword = changePasswordRequest.CurrentPassword,
        NewPassword = changePasswordRequest.NewPassword,
      }
    );

    if (result.IsFailed)
    {
      _logger.LogError("Failed to reset password: {@result}", result);
      return new ObjectResult(result);
    }

    return Created();
  }

  [HttpPost("password/reset")]
  public async Task<IActionResult> ResetPassword()
  {
    var result = await _mediator.Send(new ResetPasswordCommand());

    if (result.IsFailed)
    {
      _logger.LogError("Failed to reset password: {@result}", result);
      return new ObjectResult(result);
    }

    return Accepted();
  }

  [HttpPost("password/reset/verify")]
  public async Task<IActionResult> ResetPasswordVerify(
    [FromBody] ResetPasswordVerifyRequest resetPassword
  )
  {
    var result = await _mediator.Send(
      new ResetPasswordVerifyCommand
      {
        Otp = resetPassword.Otp,
        NewPassword = resetPassword.NewPassword,
        ConfirmNewPassword = resetPassword.ConfirmNewPassword,
      }
    );

    if (result.IsFailed)
    {
      _logger.LogError("Failed to reset password: {@result}", result);
      return new ObjectResult(result);
    }

    return Ok();
  }

  [HttpGet("products")]
  public async Task<IActionResult> GetMyProducts(
    [FromQuery] PaginationParams paginationParams,
    [FromQuery] FilterProductsQuery? filterBy = null
  )
  {
    var result = await _mediator.Send(
      new GetMyProductsQuery
      {
        PageNumber = paginationParams.PageNumber,
        PageSize = paginationParams.PageSize,
        FilterQuery = filterBy,
      }
    );

    if (result.IsFailed)
    {
      _logger.LogError("Failed to get user products: {@result}", result);
      return new ObjectResult(result);
    }

    return Ok(result.Value);
  }

  [HttpPost("wishlist")]
  public async Task<IActionResult> WishlistProduct(
    [FromBody] WishlistProductsRequest wishlistRequest
  )
  {
    var result = await _mediator.Send(
      new WishlistProductsCommand
      {
        ProductIds = wishlistRequest.ProductIds.Select(p => p.ProductId).ToList(),
      }
    );

    if (result.IsFailed)
    {
      _logger.LogError("Failed to add product to wishlist: {@result}", result);
      return new ObjectResult(result);
    }

    return Created();
  }

  [HttpGet("wishlists")]
  public async Task<IActionResult> GetWishlists([FromQuery] PaginationParams paginationParams)
  {
    var result = await _mediator.Send(
      new GetWishlistsQuery
      {
        PageNumber = paginationParams.PageNumber,
        PageSize = paginationParams.PageSize,
      }
    );

    if (result.IsFailed)
    {
      _logger.LogError("Failed to get wishlist items: {@result}", result);
      return new ObjectResult(result);
    }

    return Ok(result.Value);
  }

  [HttpDelete("wishlist")]
  public async Task<IActionResult> RemoveWishlistProducts(
    [FromBody] WishlistProductsRequest wishlistRequest
  )
  {
    var result = await _mediator.Send(
      new RemoveWishlistProductsCommand
      {
        ProductIds = wishlistRequest.ProductIds.Select(p => p.ProductId).ToList(),
      }
    );

    if (result.IsFailed)
    {
      _logger.LogError("Failed to remove products from wishlist: {@result}", result);
      return new ObjectResult(result);
    }

    return NoContent();
  }
}
