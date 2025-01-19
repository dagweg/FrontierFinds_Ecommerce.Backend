using AutoMapper;
using Ecommerce.Application.UseCases.Images.Commands;
using Ecommerce.Application.UseCases.Images.Common;

namespace Ecommerce.Application.Common.Mapping;

public class ImageMappingProfile : Profile
{
  public ImageMappingProfile()
  {
    CreateMap<CreateImageCommand, ImageResult>();
  }
}
