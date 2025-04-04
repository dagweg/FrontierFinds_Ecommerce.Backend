using Ecommerce.Domain.UserAggregate.ValueObjects;
using FluentResults;

namespace Ecommerce.Application.Common.Interfaces.Providers.Context;

// This interface is used to get the current user's ID
// This is useful when you want to know who is the current user logged in
public interface IUserContextService
{
  Result<string> GetUserId();
  Result<UserId> GetValidUserId();
}
