using AutoMapper;
using Ecommerce.Application.UseCases.Users.Common;
using Ecommerce.Domain.UserAggregate;

namespace Ecommerce.Application.Common.Mapping;

public class UserMappingProfile : Profile
{
  public UserMappingProfile()
  {
    CreateMap<User, AuthenticationResult>()
      .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value))
      .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Value))
      .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName.Value))
      .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName.Value));

    CreateMap<User, ProductReviewUserResult>()
      .ForMember(d => d.Id, o => o.MapFrom(s => s.Id.Value))
      .ForMember(d => d.Fullname, o => o.MapFrom(s => $"{s.FirstName.Value} {s.LastName.Value}"))
      .ForMember(
        d => d.ProfileImageUrl,
        o => o.MapFrom(s => s.ProfileImage != null ? s.ProfileImage.Url : null)
      );
  }
}
