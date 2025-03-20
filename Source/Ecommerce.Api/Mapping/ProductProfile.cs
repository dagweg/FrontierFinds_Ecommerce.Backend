using AutoMapper;
using Ecommerce.Application.Common.Models;
using Ecommerce.Application.UseCases.Images.Common;
using Ecommerce.Application.UseCases.Images.CreateImage;
using Ecommerce.Application.UseCases.Products.Common;
using Ecommerce.Application.UseCases.Products.CreateUser.Commands;
using Ecommerce.Contracts.Image;
using Ecommerce.Contracts.Product;
using Ecommerce.Contracts.Product.Common;
using Ecommerce.Domain.ProductAggregate.Entities;

namespace Ecommerce.Api.Mapping;

public class ProductProfile : Profile
{
  public ProductProfile()
  {
    CreateMap<CreateImageRequest, CreateImageCommand>()
      .ForMember(
        dest => dest.ImageStream,
        opt => opt.MapFrom(src => src.ImageFile.OpenReadStream())
      );

    CreateMap<ImageResult, ImageResponse>();

    // Mapping from CreateProductRequest to CreateProductCommand
    CreateMap<CreateProductRequest, CreateProductCommand>();

    CreateMap<ProductImagesResult, ProductImagesResponse>();

    CreateMap<ProductResult, ProductResponse>();

    CreateMap<ProductsResult, ProductsResponse>()
      .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products))
      .ForMember(dest => dest.TotalCount, opt => opt.MapFrom(src => src.TotalCount))
      .ForMember(dest => dest.FetchedCount, opt => opt.MapFrom(src => src.TotalFetchedCount));

    CreateMap<TagResult, TagResponse>();

    CreateMap<ProductReview, ProductReviewResult>()
      .ForMember(d => d.Rating, o => o.MapFrom(s => s.Rating.Value));

    CreateMap<GetResult<ProductReviewResult>, ProductReviewsResult>()
      .ForMember(d => d.TotalCount, o => o.MapFrom(s => s.TotalItems))
      .ForMember(d => d.TotalFetchedCount, o => o.MapFrom(s => s.TotalItemsFetched))
      .ForMember(d => d.ProductReviews, o => o.MapFrom(s => s.Items));
  }
}
