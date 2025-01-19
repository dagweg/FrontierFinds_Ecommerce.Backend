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

  public ProductController(ISender mediator, IMapper mapper)
  {
    _mediator = mediator;
    _mapper = mapper;
  }

  [HttpPost]
  public async Task<IActionResult> CreateProduct(
    [FromBody] CreateProductRequest createProductRequest
  )
  {
    var result = await _mediator.Send(_mapper.Map<CreateProductCommand>(createProductRequest));
    return Ok(_mapper.Map<ProductResponse>(result.Value));
  }
}
