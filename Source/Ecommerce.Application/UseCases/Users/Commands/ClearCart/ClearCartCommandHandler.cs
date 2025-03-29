using Ecommerce.Application.Common.Errors;
using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.Common.Interfaces.Providers.Context;
using FluentResults;
using MediatR;

namespace Ecommerce.Application.UseCases.Users.Commands.ClearCart;

public record ClearCartCommandHandler(
  IUserRepository UserRepository,
  IUserContextService UserContextService,
  IUnitOfWork UnitOfWork
) : IRequestHandler<ClearCartCommand, Result>
{
  public async Task<Result> Handle(ClearCartCommand request, CancellationToken cancellationToken)
  {
    var userId = UserContextService.GetValidUserId();
    if (userId.IsFailed)
      return userId.ToResult();

    var user = await UserRepository.GetByIdAsync(userId.Value);
    if (user == null)
      return NotFoundError.GetResult("user", "User not found");

    user.Cart.ClearCart();

    await UnitOfWork.SaveChangesAsync();
    return Result.Ok();
  }
}
