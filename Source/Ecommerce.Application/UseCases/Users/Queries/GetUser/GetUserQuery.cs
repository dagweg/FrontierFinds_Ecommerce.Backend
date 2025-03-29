using Ecommerce.Application.UseCases.Users.Common;
using FluentResults;
using MediatR;

namespace Ecommerce.Application.UseCases.Users.Queries.GetUser;

public record GetUserQuery(string UserId) : IRequest<Result<UserResult>>;
