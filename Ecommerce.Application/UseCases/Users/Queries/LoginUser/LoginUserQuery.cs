using Ecommerce.Application.UseCases.Users.Common;
using FluentResults;
using MediatR;

namespace Ecommerce.Application.UseCases.Users.Queries.LoginUser;

public record LoginUserQuery(string Email, string Password)
  : IRequest<Result<AuthenticationResult>>;
