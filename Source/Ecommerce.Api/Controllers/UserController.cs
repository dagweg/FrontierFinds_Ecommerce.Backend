namespace Ecommerce.Api.Controllers;

using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using AutoMapper;
using Ecommerce.Api.Utilities;
using Ecommerce.Application.UseCases.Products.Queries.GetAllProducts;
using Ecommerce.Application.UseCases.Users.Commands.AddToCart;
using Ecommerce.Application.UseCases.Users.Commands.ChangePassword;
using Ecommerce.Application.UseCases.Users.Commands.RegisterUser;
using Ecommerce.Application.UseCases.Users.Commands.RemoveFromCart;
using Ecommerce.Application.UseCases.Users.Commands.RemoveWishlistProducts;
using Ecommerce.Application.UseCases.Users.Commands.ResetPassword;
using Ecommerce.Application.UseCases.Users.Commands.ResetPasswordVerify;
using Ecommerce.Application.UseCases.Users.Commands.UpdateCart;
using Ecommerce.Application.UseCases.Users.Commands.WishlistProducts;
using Ecommerce.Application.UseCases.Users.Queries.GetCartItems;
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
        return Created();
    }

    [HttpDelete("cart")]
    public async Task<IActionResult> RemoveFromCart(
      [FromBody, MinLength(1)] List<RemoveCartItemRequest> removeCartItemRequest
    )
    {
        var result = await _mediator.Send(
          new RemoveFromCartCommand()
          {
              CartItemIds = removeCartItemRequest.Select(rcir => rcir.CartItemId).ToList(),
          }
        );

        if (result.IsFailed)
        {
            _logger.LogError("Failed to add product to cart: {@result}", result);
            return new ObjectResult(result);
        }
        return NoContent();
    }

    [HttpPatch("cart")]
    public async Task<IActionResult> UpdateCart(
      [FromBody, MinLength(1)] List<UpdateCartItemRequest> updateCartItemRequest
    )
    {
        var result = await _mediator.Send(
          new UpdateCartCommand()
          {
              CartItems = updateCartItemRequest
              .Select(rcir => new UpdateCartItemCommand
              {
                  CartItemId = rcir.CartItemId,
                  Quantity = rcir.Quantity,
              })
              .ToList(),
          }
        );

        if (result.IsFailed)
        {
            _logger.LogError("Failed to add product to cart: {@result}", result);
            return new ObjectResult(result);
        }
        return Ok();
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
    public async Task<IActionResult> GetMyProducts([FromQuery] PaginationParams paginationParams)
    {
        var result = await _mediator.Send(
          new GetAllProductsQuery(paginationParams.PageNumber, paginationParams.PageSize)
        );

        if (result.IsFailed)
        {
            _logger.LogError("Failed to get user products: {@result}", result);
            return new ObjectResult(result);
        }

        return Ok(_mapper.Map<ProductsResponse>(result.Value));
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
          new GetWishlistsQuery(paginationParams.PageNumber, paginationParams.PageSize)
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
