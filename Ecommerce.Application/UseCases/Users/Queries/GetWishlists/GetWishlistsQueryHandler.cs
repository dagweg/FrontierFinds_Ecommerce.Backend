using Ecommerce.Application.Common.Errors;
using Ecommerce.Application.Common.Extensions;
using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.Common.Interfaces.Providers.Context;
using Ecommerce.Application.Common.Models;
using Ecommerce.Application.UseCases.Common.Interfaces;
using Ecommerce.Application.UseCases.Products.Common;
using Ecommerce.Application.UseCases.Users.Common;
using FluentResults;
using MediatR;

namespace Ecommerce.Application.UseCases.Users.Queries.GetCartItems;

public record GetWishlistsQueryHandler : IRequestHandler<GetWishlistsQuery, Result<WishlistsResult>>
{
  private readonly IUserRepository _userRepository;
  private readonly IUserContextService _userContextService;
  private readonly IUnitOfWork _unitOfWork;
  private readonly IProductRepository _productRepository;

  public GetWishlistsQueryHandler(
    IUserRepository userRepository,
    IUserContextService userContextService,
    IUnitOfWork unitOfWork,
    IProductRepository productRepository
  )
  {
    _userRepository = userRepository;
    _userContextService = userContextService;
    _unitOfWork = unitOfWork;
    _productRepository = productRepository;
  }

  public async Task<Result<WishlistsResult>> Handle(
    GetWishlistsQuery request,
    CancellationToken cancellationToken
  )
  {
    var userIdResult = _userContextService.GetValidUserId();
    if (userIdResult.IsFailed)
      return userIdResult.ToResult();

    var result = await _userRepository.GetWishlistsAsync(
      userIdResult.Value,
      new PaginationParameters(request.PageNumber, request.PageSize)
    );

    return result is null
      ? InternalError.GetResult(
        nameof(GetWishlistsQuery),
        "A problem occured trying to get wishlists"
      )
      : Result.Ok(result);
  }
}
