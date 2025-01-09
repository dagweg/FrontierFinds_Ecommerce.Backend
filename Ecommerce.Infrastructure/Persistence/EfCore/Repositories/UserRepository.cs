namespace Ecommerce.Infrastructure.Persistence.EfCore.Repositories;

using System.Threading.Tasks;
using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Domain.Common.ValueObjects;
using Ecommerce.Domain.UserAggregate;
using Ecommerce.Domain.UserAggregate.ValueObjects;
using Microsoft.EntityFrameworkCore;

public class UserRepository(EfCoreContext context)
  : EfCoreRepository<User, UserId>(context),
    IUserRepository
{
  private readonly EfCoreContext context = context;

  public async Task<User?> GetByEmailAsync(Email email)
  {
    return await context.Users.FirstOrDefaultAsync(u => u.Email == email);
  }

  public async Task<User?> GetByPhoneNumberAsync(PhoneNumber phoneNumber)
  {
    return await context.Users.FirstOrDefaultAsync(u => u.PhoneNumber == phoneNumber);
  }
}
