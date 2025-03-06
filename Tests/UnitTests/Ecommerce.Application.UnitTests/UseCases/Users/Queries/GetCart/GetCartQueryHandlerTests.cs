using System.Threading.Tasks;
using AutoMapper;
using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.Common.Interfaces.Providers.Context;
using Ecommerce.Application.Common.Models;
using Ecommerce.Application.UseCases.Products.Common;
using Ecommerce.Application.UseCases.Users.Queries.GetCartItems;
using Ecommerce.Domain.UserAggregate.ValueObjects;
using FluentAssertions;
using FluentResults;
using Moq;

namespace Ecommerce.UnitTests.Application.UseCases.Users.Queries.GetCart;

public class GetCartQueryHandlerTests
{
  private readonly GetCartQueryHandler _handler;
  private readonly Mock<IUserRepository> _userRepositoryMock;
  private readonly Mock<IUserContextService> _userContextServiceMock;

  public GetCartQueryHandlerTests()
  {
    _userRepositoryMock = new Mock<IUserRepository>();
    _userContextServiceMock = new Mock<IUserContextService>();

    _handler = new GetCartQueryHandler(_userRepositoryMock.Object, _userContextServiceMock.Object);
  }

  [Fact]
  public async Task HandlerShould_ReturnFailed_WhenUserIsNotFound()
  {
    // Arrange
    var query = Utils.Cart.CreateGetCartQuery();

    _userContextServiceMock.Setup(x => x.GetValidUserId()).Returns(Result.Ok());

    _userRepositoryMock
      .Setup(x => x.GetCartAsync(It.IsAny<UserId>(), It.IsAny<PaginationParameters>()))
      .Returns(Task.FromResult((CartResult?)null));

    // Act
    var result = await _handler.Handle(query, default);

    // Assert
    result.IsFailed.Should().BeTrue();
  }
}
