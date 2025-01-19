using AutoMapper;
using Ecommerce.Application.UseCases.Products.Common;
using Ecommerce.Domain.ProductAggregate;

namespace Ecommerce.Application.Common.Mapping;

public class ProductMappingProfile : Profile
{
  public ProductMappingProfile()
  {
    CreateMap<Product, ProductResult>()
      .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id))
      .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Name))
      .ForMember(dest => dest.ProductDescription, opt => opt.MapFrom(src => src.Description))
      .ForMember(dest => dest.PriceValue, opt => opt.MapFrom(src => src.Price.Value))
      .ForMember(dest => dest.PriceCurrency, opt => opt.MapFrom(src => src.Price.Currency))
      .ForMember(dest => dest.StockQuantity, opt => opt.MapFrom(src => src.Stock.Quantity))
      .ForMember(
        dest => dest.Tags,
        opt => opt.MapFrom(src => src.Tags.Select(t => new TagResult { Id = t.Id, Name = t.Name }))
      );
  }
}
