using AutoMapper;
using Ecommerce.Application.Common.Errors;
using Ecommerce.Application.Common.Extensions;
using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.Common.Interfaces.Providers.Context;
using Ecommerce.Application.Common.Models;
using Ecommerce.Application.Common.Utilities;
using Ecommerce.Application.UseCases.Products.Common;
using Ecommerce.Application.UseCases.Users.Common;
using Ecommerce.Domain.ProductAggregate.ValueObjects;
using Ecommerce.Domain.UserAggregate.ValueObjects;
using FluentResults;
using MediatR;

namespace Ecommerce.Application.UseCases.Users.Queries.GetCartItems;

public class GetCartQueryHandler : IRequestHandler<GetCartQuery, Result<CartResult>>
{
  private readonly IUserRepository _userRepository;
  private readonly IUserContextService _userContext;

  public GetCartQueryHandler(IUserRepository userRepository, IUserContextService userContext)
  {
    _userRepository = userRepository;
    _userContext = userContext;
  }

  public async Task<Result<CartResult>> Handle(
    GetCartQuery request,
    CancellationToken cancellationToken
  )
  {
    var userIdResult = _userContext.GetValidUserId();
    if (userIdResult.IsFailed)
      return userIdResult.ToResult();

    // get the user cart items
    var result = await _userRepository.GetCartAsync(
      userIdResult.Value,
      new PaginationParameters { PageNumber = request.PageNumber, PageSize = request.PageSize }
    );

    if (result is null)
      return Result.Fail(new NotFoundError("User", "Couldn't get cart items"));

    return Result.Ok(result);
  }
}
