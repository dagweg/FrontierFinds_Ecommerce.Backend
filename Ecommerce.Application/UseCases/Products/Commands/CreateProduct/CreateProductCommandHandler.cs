using AutoMapper;
using Ecommerce.Application.Common.Errors;
using Ecommerce.Application.Common.Extensions;
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

namespace Ecommerce.Application.UseCases.Products.CreateUser.Commands;

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
    var thumb = _mapper.Map<ImageResult>(command.Thumbnail);
    thumb.Url = "https://aws.s3.com/1.jpg";

    var images = new ProductImagesResult();

    var name = ProductName.Create(command.ProductName);
    var description = ProductDescription.Create(command.ProductDescription);
    var stock = Stock.Create(Quantity.Create(command.StockQuantity));
    var currency = Price.ToCurrency(command.PriceCurrency);

    if (currency.IsFailed)
      return currency.ToResult();

    var price = Price.Create(command.PriceValue, currency.Value);

    var sellerId = _userContextService.GetValidUserId();

    if (sellerId.IsFailed)
      return sellerId.ToResult();

    // TODO: Have CloudStorageService to upload image given by (command.thumbnail) and return the URL
    var cloudUrlForThumbnail = "https://aws.s3.com/1.jpg";
    var thumbnail = ProductImage.Create(cloudUrlForThumbnail);

    var product = Product
      .Create(name, description, price, stock, sellerId.Value, thumbnail)
      .WithImages(
        ProductImages
          .Create()
          .WithLeftImage(command.LeftImage is null ? null : command.LeftImage.Base64)
          .WithRightImage(command.RightImage is null ? null : command.RightImage.Base64)
          .WithFrontImage(command.FrontImage is null ? null : command.FrontImage.Base64)
          .WithBackImage(command.BackImage is null ? null : command.BackImage.Base64)
      );

    // add to repo
    await _productRepository.AddAsync(product);

    // persist
    await _unitOfWork.SaveChangesAsync();

    return Result.Ok(_mapper.Map<ProductResult>(product));
  }
}
