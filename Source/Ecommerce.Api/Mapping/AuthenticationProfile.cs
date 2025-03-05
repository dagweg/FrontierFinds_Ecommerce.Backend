using AutoMapper;
using Ecommerce.Application.UseCases.Users.Commands.RegisterUser;
using Ecommerce.Application.UseCases.Users.Common;
using Ecommerce.Application.UseCases.Users.Queries.LoginUser;
using Ecommerce.Contracts.Authentication;

namespace Ecommerce.Api.Mapping;

public class AuthenticationProfile : Profile
{
  public AuthenticationProfile()
  {
    CreateMap<AuthenticationResult, AuthenticationResponse>();

    CreateMap<RegisterRequest, RegisterUserCommand>();

    CreateMap<LoginRequest, LoginUserQuery>();
  }
}
