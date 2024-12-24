using AutoMapper;
using Ecommerce.Application.UseCases.Users.Common;
using Ecommerce.Domain.User;

namespace Ecommerce.Application.Common.Mapping;

public class AuthenticationProfile : Profile
{
  public AuthenticationProfile()
  {
    CreateMap<User, AuthenticationResult>();
  }
}
