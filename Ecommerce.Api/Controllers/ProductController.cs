using AutoMapper;
using Ecommerce.Application.UseCases.Products.Commands;
using Ecommerce.Contracts.Product;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    [FromBody] CreateProductRequest createProductRequest
  )
  {
    var result = await _mediator.Send(_mapper.Map<CreateProductCommand>(createProductRequest));
    if (result.IsFailed)
    {
      _logger.LogError("Failed to create product: {@result}", result);
      return new ObjectResult(result);
    }
    return Ok(_mapper.Map<ProductResponse>(result.Value));
  }
}
