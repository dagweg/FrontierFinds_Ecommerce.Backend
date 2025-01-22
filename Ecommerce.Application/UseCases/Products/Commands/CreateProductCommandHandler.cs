using AutoMapper;
using Ecommerce.Application.Common.Errors;
using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.Common.Interfaces.Providers.Context;
using Ecommerce.Application.Common.Utilities;
using Ecommerce.Application.UseCases.Images.Common;
using Ecommerce.Application.UseCases.Products.Common;
using Ecommerce.Domain.Common.Entities;
using Ecommerce.Domain.Common.ValueObjects;
using Ecommerce.Domain.ProductAggregate;
using Ecommerce.Domain.ProductAggregate.Entities;
using Ecommerce.Domain.ProductAggregate.ValueObjects;
using Ecommerce.Domain.UserAggregate.ValueObjects;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Application.UseCases.Products.Commands;

public class CreateProductCommandHandler
  : IRequestHandler<CreateProductCommand, Result<ProductResult>>
{
  private readonly IMapper _mapper;
  private readonly IProductRepository _productRepository;
  private readonly IUnitOfWork _unitOfWork;
  private readonly IUserContextService _userContextService;
  private readonly ILogger<CreateProductCommandHandler> _logger;

  public CreateProductCommandHandler(
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

  public async Task<Result<ProductResult>> Handle(
    CreateProductCommand command,
    CancellationToken cancellationToken
  )
  {
    try
    {
      var thumb = _mapper.Map<ImageResult>(command.Thumbnail);
      thumb.Url = "https://aws.s3.com/1.jpg";

      var images = new ProductImagesResult();

      var name = ProductName.Create(command.ProductName);
      var description = ProductDescription.Create(command.ProductDescription);
      var stock = Stock.Create(Quantity.Create(command.StockQuantity));
      var currency = Price.ToCurrency(command.PriceCurrency);
      var price = Price.Create(command.PriceValue, currency);

      var sellerId = _userContextService.GetUserId();
      if (sellerId == null || Guid.TryParse(sellerId, out var sellerIdGuid) == false)
      {
        return Result.Fail(new AuthenticationError("Cookie", "Not Logged In"));
      }

      // TODO: Have CloudStorageService to upload image given by (command.thumbnail) and return the URL
      var cloudUrlForThumbnail = "https://aws.s3.com/1.jpg";
      var thumbnail = ProductImage.Create(cloudUrlForThumbnail);

      var product = Product.Create(
        name,
        description,
        price,
        stock,
        UserId.Convert(sellerIdGuid),
        thumbnail
      );

      // add to repo
      await _productRepository.AddAsync(product);

      // persist
      await _unitOfWork.SaveChangesAsync();

      return Result.Ok(_mapper.Map<ProductResult>(product));
    }
    catch (Exception ex)
    {
      _logger.LogFormattedError(ex, ex.StackTrace ?? "No stack trace available");
      throw;
    }
  }
}
