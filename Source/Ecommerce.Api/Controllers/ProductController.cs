using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Application.Common.Models;
using Ecommerce.Application.Common.Utilities;
using Ecommerce.Application.UseCases.Images.Queries.GetSupportedImageMimes;
using Ecommerce.Application.UseCases.Products.Commands.CreateReview;
using Ecommerce.Application.UseCases.Products.Commands.DeleteProduct;
using Ecommerce.Application.UseCases.Products.CreateUser.Commands;
using Ecommerce.Application.UseCases.Products.Queries.GetAllProducts;
using Ecommerce.Application.UseCases.Products.Queries.GetCategories;
using Ecommerce.Application.UseCases.Products.Queries.GetFilteredProducts;
using Ecommerce.Application.UseCases.Products.Queries.GetProductBySlug;
using Ecommerce.Application.UseCases.Products.Queries.GetReviewsByProductSlug;
using Ecommerce.Application.UseCases.Users.Commands.AddToCart;
using Ecommerce.Contracts.Cart;
using Ecommerce.Contracts.Common;
using Ecommerce.Contracts.Product;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Ecommerce.Api.Controllers;

[Authorize]
[ApiController]
[Route("products")]
public class ProductController : ControllerBase
{
  private readonly ISender _mediator;
  private readonly IMapper _mapper;
  private readonly ILogger<ProductController> _logger;

  public ProductController(ISender mediator, IMapper mapper, ILogger<ProductController> logger)
  {
    _mediator = mediator;
    _mapper = mapper;
    _logger = logger;
  }

  [HttpPost]
  public async Task<IActionResult> CreateProduct(
    [FromForm] CreateProductRequest createProductRequest
  )
  {
    LogPretty.Log(createProductRequest);
    var result = await _mediator.Send(_mapper.Map<CreateProductCommand>(createProductRequest));
    if (result.IsFailed)
    {
      _logger.LogError("Failed to create product: {@result}", result);
      return new ObjectResult(result);
    }
    return Ok(result.Value);
  }

  [AllowAnonymous]
  [HttpGet]
  public async Task<IActionResult> GetProducts([FromQuery] PaginationParams paginationParams)
  {
    var result = await _mediator.Send(
      new GetAllProductsQuery(paginationParams.PageNumber, paginationParams.PageSize)
    );

    if (result.IsFailed)
    {
      _logger.LogError("Failed to get all products: {@result}", result);
      return new ObjectResult(result);
    }

    return Ok(result.Value);
  }

  [HttpDelete]
  public async Task<IActionResult> DeleteProduct(DeleteProductRequest deleteProductRequest)
  {
    var result = await _mediator.Send(new DeleteProductCommand(deleteProductRequest.ProductIds));
    if (result.IsFailed)
    {
      _logger.LogError("Failed to delete product: {@result}", result);
      return new ObjectResult(result);
    }
    return NoContent();
  }

  [AllowAnonymous]
  [HttpGet("categories")]
  public async Task<IActionResult> GetCategories()
  {
    var result = await _mediator.Send(new GetCategoriesQuery());
    if (result.IsFailed)
    {
      _logger.LogError("Failed to get categories: {@result}", result);
      return new ObjectResult(result);
    }
    return Ok(result.Value);
  }

  [AllowAnonymous]
  [HttpGet("filter")]
  public async Task<IActionResult> GetFilteredProducts(
    [FromQuery] PaginationParameters paginationParams,
    [FromQuery] FilterProductsQuery? filterBy = null
  )
  {
    Console.WriteLine("FILTER BY");
    LogPretty.Log(filterBy);
    var result = await _mediator.Send(
      filterBy == null
        ? new FilterProductsQuery { PaginationParameters = paginationParams }
        : filterBy with
        {
          PaginationParameters = paginationParams,
        }
    );

    if (result.IsFailed)
    {
      _logger.LogError("Failed to get filtered products: {@result}", result);
      return new ObjectResult(result);
    }
    LogPretty.Log(result.Value);
    return Ok(result.Value);
  }

  [HttpGet("slug/{slug}")]
  public async Task<IActionResult> GetProductBySlug([FromRoute] string slug)
  {
    var result = await _mediator.Send(new GetProductBySlugQuery(slug));
    if (result.IsFailed)
    {
      _logger.LogError("Failed to get product by slug: {@result}", result);
      return new ObjectResult(result);
    }
    return Ok(result.Value);
  }

  [HttpGet("slug/{slug}/reviews")]
  public async Task<IActionResult> GetProductReviews(
    [FromRoute] string slug,
    [FromQuery] PaginationParameters paginationParameters
  )
  {
    var result = await _mediator.Send(
      new GetReviewsByProductSlugQuery(slug) with
      {
        PageNumber = paginationParameters.PageNumber,
        PageSize = paginationParameters.PageSize,
      }
    );
    if (result.IsFailed)
    {
      _logger.LogError("Failed to get product reviews by slug: {@result}", result);
      return new ObjectResult(result);
    }
    return Ok(result.Value);
  }

  [HttpPost("review")]
  public async Task<IActionResult> CreateProductReview(
    [FromBody] CreateReviewCommmand createReviewCommmand
  )
  {
    var result = await _mediator.Send(createReviewCommmand);

    if (result.IsFailed)
    {
      _logger.LogError("Failed to create product review: {@result}", result);
      return new ObjectResult(result);
    }

    return Ok(result.Value);
  }
}
