using AutoMapper;
using Ecommerce.Application.UseCases.Images.Common;
using Ecommerce.Application.UseCases.Products.Common;
using Ecommerce.Domain.ProductAggregate;
using Ecommerce.Domain.ProductAggregate.Entities;

namespace Ecommerce.Application.Common.Mapping;

public class ProductMappingProfile : Profile
{
  public ProductMappingProfile()
  {
    CreateMap<ProductImages, ProductImagesResult>()
      .ForMember(dest => dest.FrontImageUrl, opt => opt.MapFrom(src => src.FrontImageUrl))
      .ForMember(dest => dest.BackImageUrl, opt => opt.MapFrom(src => src.BackImageUrl))
      .ForMember(dest => dest.LeftImageUrl, opt => opt.MapFrom(src => src.LeftImageUrl))
      .ForMember(dest => dest.RightImageUrl, opt => opt.MapFrom(src => src.RightImageUrl));

    CreateMap<ProductImage, ImageResult>()
      .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url))
      .ForMember(dest => dest.FileName, opt => opt.MapFrom(src => src.FileName))
      .ForMember(dest => dest.FileSize, opt => opt.MapFrom(src => src.FileSize))
      .ForMember(dest => dest.FileType, opt => opt.MapFrom(src => src.FileType));

    CreateMap<Product, ProductResult>()
      .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id.Value))
      .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Name.Value))
      .ForMember(dest => dest.ProductDescription, opt => opt.MapFrom(src => src.Description.Value))
      .ForMember(dest => dest.PriceValue, opt => opt.MapFrom(src => src.Price.Value))
      .ForMember(dest => dest.PriceCurrency, opt => opt.MapFrom(src => src.Price.Currency))
      .ForMember(dest => dest.StockQuantity, opt => opt.MapFrom(src => src.Stock.Quantity))
      .ForMember(
        dest => dest.Tags,
        opt => opt.MapFrom(src => src.Tags.Select(t => new TagResult { Id = t.Id, Name = t.Name }))
      )
      .ForMember(dest => dest.Thumbnail, opt => opt.MapFrom(src => src.Thumbnail))
      .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images));
  }
}
