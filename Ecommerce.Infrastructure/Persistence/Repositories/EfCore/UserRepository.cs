namespace Ecommerce.Infrastructure.Persistence.Repositories.EfCore;

using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Domain.Common.ValueObjects;
using Ecommerce.Domain.User;
using Microsoft.EntityFrameworkCore;

public class UserRepository(EfCoreContext context)
  : EfCoreRepository<User, Guid>(context),
    IUserRespository
{
  private readonly EfCoreContext context = context;

  public async Task<User?> GetByEmailAsync(Email email)
  {
    return await context.Users.FirstOrDefaultAsync(u => u.Email == email);
  }
}
