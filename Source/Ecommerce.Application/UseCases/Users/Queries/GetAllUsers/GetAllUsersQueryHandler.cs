using System.Runtime.CompilerServices;
using AutoMapper;
using Ecommerce.Application.Common.Errors;
using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.Common.Interfaces.Providers.Context;
using Ecommerce.Application.Common.Models;
using Ecommerce.Application.UseCases.Common.Interfaces;
using Ecommerce.Application.UseCases.Users.Common;
using Ecommerce.Domain.UserAggregate.ValueObjects;
using FluentResults;
using MediatR;

namespace Ecommerce.Application.UseCases.Users.Queries.GetAllUsers;

public class GetAllUsersQueryHandler(
  IUserRepository userRepository,
  IUserContextService userContext,
  IMapper mapper
) : IRequestHandler<GetAllUsersQuery, Result<GetResult<UserResult>>>
{
  public async Task<Result<GetResult<UserResult>>> Handle(
    GetAllUsersQuery request,
    CancellationToken cancellationToken
  )
  {
    var userId = userContext.GetValidUserId();
    if (userId.IsFailed)
      return userId.ToResult();

    var users = await userRepository.GetAllAsync(
      new PaginationParameters { PageNumber = request.PageNumber, PageSize = request.PageSize }
    );

    return new GetResult<UserResult>
    {
      TotalItems = users.TotalItems,
      TotalItemsFetched = users.TotalItemsFetched,
      Items = users.Items.Select(u => mapper.Map<UserResult>(u)),
    };
  }
}
