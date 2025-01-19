using AutoMapper;
using Ecommerce.Application.UseCases.Images.Commands;
using Ecommerce.Application.UseCases.Images.Common;
using Ecommerce.Application.UseCases.Products.Commands;
using Ecommerce.Application.UseCases.Products.Common;
using Ecommerce.Contracts.Image;
using Ecommerce.Contracts.Product;
using Ecommerce.Contracts.Product.Common;
using Ecommerce.Domain.ProductAggregate.Entities;

namespace Ecommerce.Api.Mapping;

public class ProductProfile : Profile
{
  public ProductProfile()
  {
    CreateMap<CreateImageRequest, CreateImageCommand>();
    CreateMap<ImageResult, ImageResponse>();
    CreateMap<CreateProductRequest, CreateProductCommand>();
    CreateMap<ProductResult, ProductResponse>();
    CreateMap<ProductImagesResult, ProductImagesResponse>();
    CreateMap<TagResult, TagResponse>();
  }
}
