using AutoMapper;
using Ecommerce.Application.Common.Errors;
using Ecommerce.Application.Common.Extensions;
using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.Common.Interfaces.Providers.Context;
using Ecommerce.Application.Common.Utilities;
using Ecommerce.Application.UseCases.Products.Commands.DeleteProduct;
using Ecommerce.Domain.ProductAggregate.ValueObjects;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Application.UseCases.Products.CreateUser.Commands;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result>
{
  private readonly IMapper _mapper;
  private readonly IProductRepository _productRepository;
  private readonly IUnitOfWork _unitOfWork;
  private readonly IUserContextService _userContextService;
  private readonly ILogger<CreateProductCommandHandler> _logger;

  public DeleteProductCommandHandler(
    IMapper mapper,
    IProductRepository productRepository,
    IUnitOfWork unitOfWork,
    IUserContextService userContextService,
    ILogger<CreateProductCommandHandler> logger
  )
  {
    _mapper = mapper;
    _productRepository = productRepository;
    _unitOfWork = unitOfWork;
    _userContextService = userContextService;
    _logger = logger;
  }

  public async Task<Result> Handle(
    DeleteProductCommand command,
    CancellationToken cancellationToken
  )
  {
    var userId = _userContextService.GetValidUserId();

    if (userId.IsFailed)
      return userId.ToResult();

    var productIdGuid = ConversionUtility.ToGuid(command.ProductId);

    if (productIdGuid.IsFailed)
      return productIdGuid.ToResult();

    var productId = ProductId.Convert(productIdGuid.Value);
    var product = await _productRepository.GetByIdAsync(productId);

    if (product is null)
      return Result.Fail(new NotFoundError(nameof(product), "Product not found"));

    var deletedProd = _productRepository.Delete(product);

    if (!deletedProd)
      return Result.Fail(new InternalError("Failed to delete product"));

    await _unitOfWork.SaveChangesAsync(cancellationToken);

    return Result.Ok();
  }
}
