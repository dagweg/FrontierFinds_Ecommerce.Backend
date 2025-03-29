using AutoMapper;
using Ecommerce.Application.Common.Errors;
using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.Common.Interfaces.Providers.Context;
using Ecommerce.Application.Common.Utilities;
using Ecommerce.Application.UseCases.Common.Interfaces;
using Ecommerce.Application.UseCases.Users.Common;
using Ecommerce.Domain.UserAggregate.ValueObjects;
using FluentResults;
using MediatR;

namespace Ecommerce.Application.UseCases.Users.Queries.GetUser;

public class GetUserQueryHandler(
  IUserRepository userRepository,
  IUserContextService userContext,
  IMapper mapper
) : IRequestHandler<GetUserQuery, Result<UserResult>>
{
  public async Task<Result<UserResult>> Handle(
    GetUserQuery request,
    CancellationToken cancellationToken
  )
  {
    var userId = userContext.GetValidUserId();
    if (userId.IsFailed)
      return userId.ToResult();

    var user = await userRepository.GetByIdAsync(
      UserId.Convert(ConversionUtility.ToGuid(request.UserId).Value)
    );

    if (user is null)
      return NotFoundError.GetResult("user", "User not found");

    return mapper.Map<UserResult>(user);
  }
}
