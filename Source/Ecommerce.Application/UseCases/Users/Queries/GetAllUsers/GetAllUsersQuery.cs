using Ecommerce.Application.Common.Models;
using Ecommerce.Application.UseCases.Users.Common;
using FluentResults;
using MediatR;

namespace Ecommerce.Application.UseCases.Users.Queries.GetAllUsers;

public record GetAllUsersQuery()
  : PaginationParametersImmutable,
    IRequest<Result<GetResult<UserResult>>>;
