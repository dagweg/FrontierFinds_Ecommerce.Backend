using AutoMapper;
using Ecommerce.Application.Behaviors.Strategies.ProductImageStrategies;
using Ecommerce.Application.Common.Defaults;
using Ecommerce.Application.Common.Errors;
using Ecommerce.Application.Common.Extensions;
using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.Common.Interfaces.Providers.Context;
using Ecommerce.Application.Common.Interfaces.Providers.Forex;
using Ecommerce.Application.Common.Interfaces.Providers.Search.Elastic;
using Ecommerce.Application.Common.Interfaces.Storage;
using Ecommerce.Application.Common.Models.Search.Elastic.Documents;
using Ecommerce.Application.Common.Models.Storage;
using Ecommerce.Application.Common.Utilities;
using Ecommerce.Application.Services.Utilities;
using Ecommerce.Application.Services.Workers.Elastic;
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
  private readonly ISender _sender;
  private readonly IPublisher _publisher;
  private readonly ILogger<CreateProductCommandHandler> _logger;
  private readonly IForexSerivce _forexService;
  private readonly ISlugService<ProductId> _slugService;
  private readonly IProductImageStrategyResolver _productImageStrategyResolver;
  private readonly IElasticSearch _elastic;

  public CreateProductCommandHandler(
    IMapper mapper,
    IProductRepository productRepository,
    ISlugService<ProductId> slugService,
    IUnitOfWork unitOfWork,
    IUserContextService userContextService,
    ISender sender,
    IPublisher publisher,
    ILogger<CreateProductCommandHandler> logger,
    IForexSerivce forexService,
    IProductImageStrategyResolver productImageStrategyResolver,
    IElasticSearch elastic
  )
  {
    _mapper = mapper;
    _productRepository = productRepository;
    _unitOfWork = unitOfWork;
    _slugService = slugService;
    _userContextService = userContextService;
    _sender = sender;
    _publisher = publisher;
    _logger = logger;
    _forexService = forexService;
    _productImageStrategyResolver = productImageStrategyResolver;
    _elastic = elastic;
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

    var forexExchangeResult = Result.Ok(command.PriceValueInCents);
    if (currency.Value != Price.BASE_CURRENCY)
    {
      // convert the price to base currency before creating the product
      forexExchangeResult = await _forexService.ConvertToBaseCurrencyAsync(
        command.PriceValueInCents,
        currency.Value
      );

      if (forexExchangeResult.IsFailed)
        return forexExchangeResult.ToResult();
    }

    var priceInBaseCurrency = Price.CreateInBaseCurrency(forexExchangeResult.Value);

    // Upload the images to cloud storage

    var images = new Dictionary<ProductView, CreateImageCommand?>
    {
      { ProductView.Thumbnail, command.Thumbnail },
      { ProductView.Left, command.LeftImage },
      { ProductView.Right, command.RightImage },
      { ProductView.Front, command.FrontImage },
      { ProductView.Back, command.BackImage },
      { ProductView.Top, command.TopImage },
      { ProductView.Bottom, command.BottomImage },
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

    var categories = await _productRepository.GetCategoriesById(command.Categories);
    var tags = await _productRepository.GetOrCreateTags(command.Tags ?? []);

    var slug = await _slugService.GenerateUniqueSlugAsync(name.Value ?? Guid.NewGuid().ToString());

    var product = Product
      .Create(
        name,
        slug: slug.Value,
        description,
        priceInBaseCurrency,
        stock,
        sellerId.Value,
        productImages.Thumbnail
      )
      .WithImages(productImages)
      .WithCategories(categories.ToList())
      .WithTags(tags.ToList());

    // Add the product to the repository
    await _productRepository.AddAsync(product);

    await _unitOfWork.SaveChangesAsync();

    // publish elastic search indexing task to background task queue
    await _publisher.Publish(
      new ElasticTaskNotification()
      {
        ElasticAction = ElasticAction.Index,
        IndexDocs = new Dictionary<string, ElasticDocumentBase[]>
        {
          { ElasticIndices.ProductIndex, [_mapper.Map<ProductDocument>(product)] },
        },
      }
    );

    return Result.Ok(_mapper.Map<ProductResult>(product));
  }
}
