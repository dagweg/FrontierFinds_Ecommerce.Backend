using AutoMapper;
using Ecommerce.Application.Behaviors.Strategies.ProductImageStrategies;
using Ecommerce.Application.Common.Errors;
using Ecommerce.Application.Common.Extensions;
using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.Common.Interfaces.Providers.Context;
using Ecommerce.Application.Common.Interfaces.Providers.Forex;
using Ecommerce.Application.Common.Interfaces.Storage;
using Ecommerce.Application.Common.Models.Storage;
using Ecommerce.Application.Common.Utilities;
using Ecommerce.Application.UseCases.Images.Common;
using Ecommerce.Application.UseCases.Images.CreateImage;
using Ecommerce.Application.UseCases.Products.Common;
using Ecommerce.Domain.Common.Entities;
using Ecommerce.Domain.Common.ValueObjects;
using Ecommerce.Domain.ProductAggregate;
using Ecommerce.Domain.ProductAggregate.Entities;
using Ecommerce.Domain.ProductAggregate.Enums;
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
    private readonly IMediator _sender;
    private readonly ILogger<CreateProductCommandHandler> _logger;
    private readonly IForexSerivce _forexService;
    private readonly IProductImageStrategyResolver _productImageStrategyResolver;

    public CreateProductCommandHandler(
      IMapper mapper,
      IProductRepository productRepository,
      IUnitOfWork unitOfWork,
      IUserContextService userContextService,
      IMediator sender,
      ILogger<CreateProductCommandHandler> logger,
      IForexSerivce forexService,
      IProductImageStrategyResolver productImageStrategyResolver
    )
    {
        _mapper = mapper;
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
        _userContextService = userContextService;
        _sender = sender;
        _logger = logger;
        _forexService = forexService;
        _productImageStrategyResolver = productImageStrategyResolver;
    }

    public async Task<Result<ProductResult>> Handle(
      CreateProductCommand command,
      CancellationToken cancellationToken
    )
    {
        var sellerId = _userContextService.GetValidUserId();

        if (sellerId.IsFailed)
            return sellerId.ToResult();

        var name = ProductName.Create(command.ProductName);
        var description = ProductDescription.Create(command.ProductDescription);
        var stock = Stock.Create(Quantity.Create(command.StockQuantity));
        var currency = Price.ToCurrency(command.PriceCurrency);

        if (currency.IsFailed)
            return currency.ToResult();

        // convert the price to base currency before creating the product
        var forexExchangeResult = await _forexService.ConvertToBaseCurrencyAsync(
          command.PriceValue,
          currency.Value
        );

        if (forexExchangeResult.IsFailed)
            return forexExchangeResult.ToResult();

        var priceInBaseCurrency = Price.CreateInBaseCurrency(forexExchangeResult.Value);

        // Upload the images to cloud storage

        var images = new Dictionary<ProductView, CreateImageCommand?>
    {
      { ProductView.Thumbnail, command.Thumbnail },
      { ProductView.Left, command.LeftImage },
      { ProductView.Right, command.RightImage },
      { ProductView.Front, command.FrontImage },
      { ProductView.Back, command.BackImage },
    };

        ProductImages productImages = ProductImages.Create(ProductImage.Create("", ""));

        /// upload images and construct <see cref="ProductImages"/>
        foreach (var (view, image) in images)
        {
            if (image is not null)
            {
                var imageResult = await _sender.Send(image);

                // return if upload fails; compensation will be handled by the pipeline
                if (imageResult.IsFailed)
                    return imageResult.ToResult();

                // resolve the strategy for the given product view
                var strategy = _productImageStrategyResolver.Resolve(view);

                if (strategy == null)
                {
                    _logger.LogError("No strategy found for {@view}", view);
                    continue;
                }

                // apply the appropriate strategy for the given view
                strategy.Apply(
                  productImages,
                  ProductImage.Create(imageResult.Value.Url, imageResult.Value.ObjectIdentifier)
                );
            }
        }

        var product = Product
          .Create(
            name,
            description,
            priceInBaseCurrency,
            stock,
            sellerId.Value,
            productImages.Thumbnail
          )
          .WithImages(productImages);

        // add to repo
        await _productRepository.AddAsync(product);

        // persist
        await _unitOfWork.SaveChangesAsync();

        return Result.Ok(_mapper.Map<ProductResult>(product));
    }
}
