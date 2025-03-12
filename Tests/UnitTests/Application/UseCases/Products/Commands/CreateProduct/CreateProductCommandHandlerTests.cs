using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Api.Services;
using Ecommerce.Application.Behaviors.Strategies.ProductImageStrategies;
using Ecommerce.Application.Common.Extensions;
using Ecommerce.Application.Common.Interfaces.Authentication;
using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.Common.Interfaces.Providers.Context;
using Ecommerce.Application.Common.Interfaces.Providers.Forex;
using Ecommerce.Application.Common.Interfaces.Providers.Localization;
using Ecommerce.Application.Common.Interfaces.Strategies;
using Ecommerce.Application.Common.Interfaces.Validation;
using Ecommerce.Application.UseCases.Images.Common;
using Ecommerce.Application.UseCases.Images.CreateImage;
using Ecommerce.Application.UseCases.Products.Common;
using Ecommerce.Application.UseCases.Products.CreateUser.Commands;
using Ecommerce.Application.UseCases.Users.Commands.RegisterUser;
using Ecommerce.Domain.Common.Enums;
using Ecommerce.Domain.Common.ValueObjects;
using Ecommerce.Domain.ProductAggregate;
using Ecommerce.Domain.ProductAggregate.Entities;
using Ecommerce.Domain.ProductAggregate.Enums;
using Ecommerce.Domain.ProductAggregate.ValueObjects;
using Ecommerce.Domain.UserAggregate;
using Ecommerce.Domain.UserAggregate.ValueObjects;
using Ecommerce.Tests.Shared;
using FluentAssertions;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.Extensions.Logging;
using Moq;

namespace Ecommerce.Application.UnitTests.UseCases.Products.Commands.CreateProduct
{
  public class CreateProductCommandHandlerTests
  {
    private readonly CreateProductCommandHandler _createProductCommandHandler;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IProductRepository> _productRepositoryMock;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IUserContextService> _userContextServiceMock;
    private readonly Mock<IMediator> _senderMock;
    private readonly Mock<ILogger<CreateProductCommandHandler>> _loggerMock;
    private readonly Mock<IForexSerivce> _forexServiceMock;
    private readonly Mock<IProductImageStrategyResolver> _productImageStrategyResolver;

    public CreateProductCommandHandlerTests()
    {
      _mapperMock = new Mock<IMapper>();
      _productRepositoryMock = new Mock<IProductRepository>();
      _unitOfWorkMock = new Mock<IUnitOfWork>();
      _userContextServiceMock = new Mock<IUserContextService>();
      _senderMock = new Mock<IMediator>();
      _loggerMock = new Mock<ILogger<CreateProductCommandHandler>>();
      _forexServiceMock = new Mock<IForexSerivce>();
      _productImageStrategyResolver = new Mock<IProductImageStrategyResolver>();

      _createProductCommandHandler = new(
        _mapperMock.Object,
        _productRepositoryMock.Object,
        _unitOfWorkMock.Object,
        _userContextServiceMock.Object,
        _senderMock.Object,
        _loggerMock.Object,
        _forexServiceMock.Object,
        _productImageStrategyResolver.Object
      );
    }

    [Fact]
    public async Task HandlerShould_ReturnFailed_WhenUserIsNotLoggedIn()
    {
      // arrange
      var command = Utils.Product.CreateCommand();

      string errMsg = "User id not found";
      // configure the userContextMock to return failed
      _userContextServiceMock.Setup(x => x.GetValidUserId()).Returns(Result.Fail(errMsg));
      // act
      var result = await _createProductCommandHandler.Handle(command, default);

      // assert
      result.IsFailed.Should().BeTrue();
      result.Errors.Find(e => e.Message == errMsg).Should().NotBeNull();
    }

    [Fact]
    public async Task HandlerShould_ReturnFailed_WhenCurrencyIsInvalid()
    {
      //arrange
      var command = Utils.Product.CreateCommand() with
      {
        PriceCurrency = "invalid_currency",
      };

      _userContextServiceMock.Setup(x => x.GetValidUserId()).Returns(Result.Ok());

      //act
      var result = await _createProductCommandHandler.Handle(command, default);

      // assert
      result.IsFailed.Should().BeTrue();
    }

    [Fact]
    public async Task HandlerShould_ReturnOk_WhenCommandIsValid()
    {
      //arrange
      var command = Utils.Product.CreateCommand();

      _userContextServiceMock.Setup(x => x.GetValidUserId()).Returns(Result.Ok());
      _forexServiceMock
        .Setup(x => x.ConvertToBaseCurrencyAsync(It.IsAny<long>(), It.IsAny<Currency>()))
        .ReturnsAsync(Result.Ok());

      _mapperMock
        .Setup(x => x.Map<Domain.ProductAggregate.Product>(It.IsAny<CreateProductCommand>()))
        .Returns(
          (CreateProductCommand cmd) =>
            Domain.ProductAggregate.Product.Create(
              ProductName.Create(cmd.ProductName),
              ProductDescription.Create(cmd.ProductDescription),
              Price.CreateInBaseCurrency(100),
              Stock.Create(Quantity.Create(cmd.StockQuantity)),
              UserId.CreateUnique(),
              ProductImage.Create("", "")
            )
        );

      _productRepositoryMock
        .Setup(x => x.AddAsync(It.IsAny<Domain.ProductAggregate.Product>()))
        .ReturnsAsync(true);

      _unitOfWorkMock.Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>())).ReturnsAsync(1);

      var dummyImageResult = new ImageResult
      {
        Url = "http://example.com/image.jpg",
        ObjectIdentifier = Guid.NewGuid().ToString(),
      };

      _senderMock
        .Setup(x => x.Send(It.IsAny<CreateImageCommand>(), It.IsAny<CancellationToken>())) // Match the command type
        .ReturnsAsync(Result.Ok(dummyImageResult));

      var mockProductImageStrategy = new Mock<IProductImageStrategy>();

      _productImageStrategyResolver
        .Setup(x => x.Resolve(It.IsAny<ProductView>()))
        .Returns(mockProductImageStrategy.Object);

      mockProductImageStrategy
        .Setup(x => x.Apply(It.IsAny<ProductImages>(), It.IsAny<ProductImage>()))
        .Callback<ProductImages, ProductImage>(
          (images, image) =>
          {
            images.WithThumbnail(image); // Simulate setting Thumbnail
          }
        );

      // _mapperMock
      //   .Setup(x => x.Map<ProductResult>(It.IsAny<Product>()))
      //   .Returns(
      //     new ProductResult
      //     { /* Populate as needed */
      //     }
      //   );

      //act
      var result = await _createProductCommandHandler.Handle(command, default);

      // assert
      result.IsSuccess.Should().BeTrue();
    }
  }
}
