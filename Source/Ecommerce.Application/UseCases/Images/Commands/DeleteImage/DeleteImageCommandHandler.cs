using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.Common.Interfaces.Storage;
using Ecommerce.Application.Common.Models.Storage;
using Ecommerce.Application.UseCases.Images.Commands.DeleteImage;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Application.UseCases.Images.Commands.DeleteImage;

public class DeleteImageCommandHandler : IRequestHandler<DeleteImageCommand, Result>
{
    private readonly IProductRepository _productRepository;
    private readonly ICloudStorageService _cloudStorage;
    private readonly ILogger<DeleteImageCommandHandler> _logger;

    public DeleteImageCommandHandler(
      IProductRepository productRepository,
      ICloudStorageService cloudStorage,
      ILogger<DeleteImageCommandHandler> logger
    )
    {
        _productRepository = productRepository;
        _cloudStorage = cloudStorage;
        _logger = logger;
    }

    public async Task<Result> Handle(DeleteImageCommand command, CancellationToken cancellationToken)
    {
        var deletionResult = await _cloudStorage.DeleteImageAsync(
          new DeleteImageParams(command.ObjectIdentifier)
        );
        return deletionResult;
    }
}
