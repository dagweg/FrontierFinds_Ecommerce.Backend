using AutoMapper;
using Ecommerce.Application.UseCases.Users.Common;
using Ecommerce.Domain.UserAggregate;

namespace Ecommerce.Application.Common.Mapping;

public class AuthenticationProfile : Profile
{
  public AuthenticationProfile()
  {
    CreateMap<User, AuthenticationResult>();
  }
}
