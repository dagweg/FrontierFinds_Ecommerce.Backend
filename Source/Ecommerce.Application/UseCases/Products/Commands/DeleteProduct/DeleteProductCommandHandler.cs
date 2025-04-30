using AutoMapper;
using Ecommerce.Application.Common.Errors;
using Ecommerce.Application.Common.Extensions;
using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.Common.Interfaces.Providers.Context;
using Ecommerce.Application.Common.Models;
using Ecommerce.Application.Common.Utilities;
using Ecommerce.Application.Services.Workers;
using Ecommerce.Application.Services.Workers.Cloudinary;
using Ecommerce.Application.UseCases.Products.Commands.DeleteProduct;
using Ecommerce.Domain.ProductAggregate.ValueObjects;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Application.UseCases.Products.CreateUser.Commands;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result>
{
  private readonly IPublisher _publisher;
  private readonly IMapper _mapper;
  private readonly IProductRepository _productRepository;
  private readonly IUnitOfWork _unitOfWork;
  private readonly IUserContextService _userContextService;
  private readonly ILogger<CreateProductCommandHandler> _logger;

  public DeleteProductCommandHandler(
    IPublisher publisher,
    IMapper mapper,
    IProductRepository productRepository,
    IUnitOfWork unitOfWork,
    IUserContextService userContextService,
    ILogger<CreateProductCommandHandler> logger
  )
  {
    _publisher = publisher;
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
    try
    {
      var userId = _userContextService.GetValidUserId();
      if (userId.IsFailed)
        return userId.ToResult();

      Result<List<ProductId>> productIdsR = ConversionUtility.ToProductIds(command.productIds);
      if (productIdsR.IsFailed)
        return productIdsR.ToResult();

      // Use ExecuteTransactionAsync to wrap the entire operation
      Result<DeleteResult>? deleteResult = null;
      await _unitOfWork.ExecuteTransactionAsync(
        async () =>
        {
          deleteResult = await _productRepository.BulkDeleteByIdAsync(productIdsR.Value);

          if (deleteResult.IsFailed)
            return 1;

          if (deleteResult.Value.CleanupObjectIds.Any())
          {
            await _publisher.Publish(
              new CloudinaryTaskNotification
              {
                CloudinaryAction = CloudinaryAction.Delete,
                ObjectIds = deleteResult.Value.CleanupObjectIds.ToList(),
              },
              cancellationToken
            );
          }
          return 0; // Return a dummy value, as we are interested in Result.Ok()
        },
        cancellationToken
      );

      if (deleteResult != null && deleteResult.IsFailed)
        return deleteResult.ToResult();

      return Result.Ok(); // Operation successful
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Error during product deletion process. Transaction rolled back.");
      return InternalError.GetResult(
        "An error occurred during product deletion. Please try again later."
      );
    }
  }
}
