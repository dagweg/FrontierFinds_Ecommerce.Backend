namespace Ecommerce.Infrastructure.Persistence.EfCore.Repositories;

using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Domain.Common.ValueObjects;
using Ecommerce.Domain.UserAggregate;
using Ecommerce.Domain.UserAggregate.ValueObjects;

public class UserRepository(EfCoreContext context)
  : EfCoreRepository<User, UserId>(context),
    IUserRepository
{
  private readonly EfCoreContext context = context;

  public async Task<User?> GetByEmailAsync(Email email)
  {
    await Task.CompletedTask; // Don't foget to remove!!

    return null;
    // return User.Create(Name.Empty, Name.Empty, Email.Empty, Password.Empty);
    // return await context.Users.FirstOrDefaultAsync(u => u.Email == email);
  }
}
