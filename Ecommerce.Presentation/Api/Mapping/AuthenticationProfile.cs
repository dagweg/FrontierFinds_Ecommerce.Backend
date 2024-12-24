using AutoMapper;
using Ecommerce.Application.UseCases.Users.Commands.RegisterUser;
using Ecommerce.Application.UseCases.Users.Common;
using Ecommerce.Application.UseCases.Users.Queries.LoginUser;
using Ecommerce.Presentation.Contracts.Authentication;

namespace Ecommerce.Presentation.Api.Mapping;

public class AuthenticationProfile : Profile
{
  public AuthenticationProfile()
  {
    CreateMap<AuthenticationResult, AuthenticationResponse>();

    CreateMap<RegisterRequest, RegisterUserCommand>();

    CreateMap<LoginRequest, LoginUserQuery>();
  }
}
