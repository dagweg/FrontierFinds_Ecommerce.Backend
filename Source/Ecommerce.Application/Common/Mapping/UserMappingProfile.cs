using AutoMapper;
using Ecommerce.Application.Common.Models;
using Ecommerce.Application.UseCases.Products.Common;
using Ecommerce.Application.UseCases.Users.Common;
using Ecommerce.Domain.Common.ValueObjects;
using Ecommerce.Domain.UserAggregate;
using Ecommerce.Domain.UserAggregate.Entities;

namespace Ecommerce.Application.Common.Mapping;

public class UserMappingProfile : Profile
{
  public UserMappingProfile()
  {
    CreateMap<Address, AddressResult>()
      .ForMember(d => d.City, o => o.MapFrom(s => s.City))
      .ForMember(d => d.Country, o => o.MapFrom(s => s.Country))
      .ForMember(d => d.State, o => o.MapFrom(s => s.State))
      .ForMember(d => d.Street, o => o.MapFrom(s => s.Street))
      .ForMember(d => d.ZipCode, o => o.MapFrom(s => s.ZipCode));

    CreateMap<User, UserResult>()
      .ForMember(d => d.UserId, o => o.MapFrom(s => s.Id.Value))
      .ForMember(d => d.FirstName, o => o.MapFrom(s => s.FirstName.Value))
      .ForMember(d => d.LastName, o => o.MapFrom(s => s.LastName.Value))
      .ForMember(d => d.Email, o => o.MapFrom(s => s.Email.Value))
      .ForMember(d => d.PhoneNumber, o => o.MapFrom(s => s.PhoneNumber.Value))
      .ForMember(d => d.Address, o => o.MapFrom(s => s.Address)) // Assuming you have a mapping for UserAddress to AddressResult
      .ForMember(d => d.AccountVerified, o => o.MapFrom(s => s.AccountVerified))
      .ForMember(d => d.ProfileImage, o => o.MapFrom(s => s.ProfileImage)); // Assuming a Map for Image to ImageResult

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
