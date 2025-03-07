using System.Threading.Tasks;
using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.Common.Interfaces.Providers.Context;
using Ecommerce.Application.UseCases.Users.Commands.WishlistProducts;
using Ecommerce.Domain.ProductAggregate.ValueObjects;
using Ecommerce.Domain.UserAggregate.ValueObjects;
using FluentAssertions;
using FluentResults;
using Moq;

namespace Ecommerce.UnitTests.Application.UseCases.Users.Commands.WishlistProducts;

public class WishlistProductsCommandHandlerTests
{
  private readonly WishlistProductsCommandHandler _wishlistProductsCommandHandler;
  private readonly Mock<IUserRepository> _userRepositoryMock;
  private readonly Mock<IUserContextService> _userContextServiceMock;
  private readonly Mock<IUnitOfWork> _unitOfWorkMock;
  private readonly Mock<IProductRepository> _productRepositoryMock;

  public WishlistProductsCommandHandlerTests()
  {
    _userRepositoryMock = new Mock<IUserRepository>();
    _userContextServiceMock = new Mock<IUserContextService>();
    _unitOfWorkMock = new Mock<IUnitOfWork>();
    _productRepositoryMock = new Mock<IProductRepository>();

    _userContextServiceMock.Setup(x => x.GetValidUserId()).Returns(Result.Ok());

    _wishlistProductsCommandHandler = new WishlistProductsCommandHandler(
      userRepository: _userRepositoryMock.Object,
      userContextService: _userContextServiceMock.Object,
      unitOfWork: _unitOfWorkMock.Object,
      productRepository: _productRepositoryMock.Object
    );
  }

  [Fact]
  public async Task HandlerShould_ReturnSuccess_WhenWishlistsAreAdded()
  {
    // Given
    var command = Utils.Wishlist.CreateWishlistProductsCommand();

    _userRepositoryMock
      .Setup(x => x.AddToWishlistRangeAsync(It.IsAny<UserId>(), It.IsAny<List<ProductId>>()))
      .ReturnsAsync(true);

    // When
    var result = await _wishlistProductsCommandHandler.Handle(command, default);

    // Then
    result.IsSuccess.Should().BeTrue();
  }

  [Fact]
  public async Task HandlerShould_ReturnFailure_WhenWishlistProductIdIsInvalid()
  {
    // Given
    var command = Utils.Wishlist.CreateWishlistProductsCommand() with
    {
      ProductIds = new List<string> { "invalid-id" },
    };

    // When
    var result = await _wishlistProductsCommandHandler.Handle(command, default);

    // Then
    result.IsFailed.Should().BeTrue();
  }
}
