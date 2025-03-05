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
          .ForMember(dest => dest.Thumbnail, opt => opt.MapFrom(src => src.Thumbnail))
          .ForMember(dest => dest.FrontImage, opt => opt.MapFrom(src => src.FrontImage))
          .ForMember(dest => dest.BackImage, opt => opt.MapFrom(src => src.BackImage))
          .ForMember(dest => dest.LeftImage, opt => opt.MapFrom(src => src.LeftImage))
          .ForMember(dest => dest.RightImage, opt => opt.MapFrom(src => src.RightImage))
          .ForMember(dest => dest.TopImage, opt => opt.MapFrom(src => src.TopImage))
          .ForMember(dest => dest.BottomImage, opt => opt.MapFrom(src => src.BottomImage));

        CreateMap<ProductImage, ProductImageResult>()
          .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url));

        CreateMap<ProductTag, TagResult>();

        CreateMap<Product, ProductResult>()
          .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id.Value))
          .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Name.Value))
          .ForMember(dest => dest.ProductDescription, opt => opt.MapFrom(src => src.Description.Value))
          .ForMember(dest => dest.PriceValue, opt => opt.MapFrom(src => src.Price.Value))
          .ForMember(dest => dest.StockQuantity, opt => opt.MapFrom(src => src.Stock.Quantity))
          .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags))
          .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images));
    }
}
