using AutoMapper;
using Ecommerce.Application.Common.Models;
using Ecommerce.Application.Common.Models.Search.Elastic.Documents;
using Ecommerce.Application.UseCases.Images.Common;
using Ecommerce.Application.UseCases.Products.Common;
using Ecommerce.Domain.Common.Entities;
using Ecommerce.Domain.ProductAggregate;
using Ecommerce.Domain.ProductAggregate.Entities;

namespace Ecommerce.Application.Common.Mapping;

public class ProductMappingProfile : Profile
{
  public ProductMappingProfile()
  {
    CreateMap<ProductImage, ProductImageResult>()
      .ForMember(dest => dest.Url, opt => opt.MapFrom(src => src.Url));
    CreateMap<ProductTag, TagResult>();
    CreateMap<Category, CategoryResult>();
    CreateMap<ProductReview, ProductReviewResult>();
    CreateMap<Promotion, PromotionResult>();

    CreateMap<ProductImages, ProductImagesResult>()
      .ForMember(dest => dest.Thumbnail, opt => opt.MapFrom(src => src.Thumbnail))
      .ForMember(dest => dest.FrontImage, opt => opt.MapFrom(src => src.FrontImage))
      .ForMember(dest => dest.BackImage, opt => opt.MapFrom(src => src.BackImage))
      .ForMember(dest => dest.LeftImage, opt => opt.MapFrom(src => src.LeftImage))
      .ForMember(dest => dest.RightImage, opt => opt.MapFrom(src => src.RightImage))
      .ForMember(dest => dest.TopImage, opt => opt.MapFrom(src => src.TopImage))
      .ForMember(dest => dest.BottomImage, opt => opt.MapFrom(src => src.BottomImage));

    CreateMap<Product, ProductResult>()
      .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.Id.Value))
      .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Name.Value))
      .ForMember(dest => dest.ProductDescription, opt => opt.MapFrom(src => src.Description.Value))
      .ForMember(dest => dest.PriceValueInCents, opt => opt.MapFrom(src => src.Price.ValueInCents))
      .ForMember(dest => dest.StockQuantity, opt => opt.MapFrom(src => src.Stock.Quantity))
      .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags))
      .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories))
      .ForMember(dest => dest.SellerId, opt => opt.MapFrom(src => src.SellerId))
      .ForMember(dest => dest.TotalReviews, opt => opt.MapFrom(src => src.TotalReviews))
      .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.Images))
      .ForMember(dest => dest.Promotion, opt => opt.MapFrom(src => src.Promotion))
      .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src => src.AverageRatingValue));

    CreateMap<GetResult<ProductReview>, ProductReviewsResult>()
      .ForMember(dest => dest.ProductReviews, opt => opt.MapFrom(src => src.Items))
      .ForMember(dest => dest.TotalCount, opt => opt.MapFrom(src => src.TotalItems))
      .ForMember(dest => dest.TotalFetchedCount, opt => opt.MapFrom(src => src.TotalItemsFetched));

    CreateMap<ProductImages, ProductImagesDocument>();
    CreateMap<ProductImage, ProductImageDocument>();
    CreateMap<Promotion, PromotionDocument>();
    CreateMap<ProductTag, TagDocument>();
    CreateMap<Category, CategoryDocument>();

    CreateMap<ProductImagesDocument, ProductImagesResult>();
    CreateMap<ProductImageDocument, ProductImageResult>();
    CreateMap<PromotionDocument, PromotionResult>();
    CreateMap<TagDocument, TagResult>();
    CreateMap<CategoryDocument, CategoryResult>();

    CreateMap<Product, ProductDocument>()
      .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.Value.ToString()))
      .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name.Value))
      .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description.Value))
      .ForMember(dest => dest.PriceValueInCents, opt => opt.MapFrom(src => src.Price.ValueInCents))
      .ForMember(dest => dest.StockQuantity, opt => opt.MapFrom(src => src.Stock.Quantity))
      .ForMember(dest => dest.Slug, opt => opt.MapFrom(src => src.Slug))
      .ForMember(dest => dest.SellerId, opt => opt.MapFrom(src => src.SellerId.Value))
      .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories))
      .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags))
      .ForMember(dest => dest.TotalReviews, opt => opt.MapFrom(src => src.TotalReviews))
      .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src => src.AverageRatingValue))
      .ForMember(dest => dest.ProductImages, opt => opt.MapFrom(src => src.Images))
      .ForMember(dest => dest.Promotion, opt => opt.MapFrom(src => src.Promotion))
      .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
      .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => src.UpdatedAt));

    CreateMap<ProductDocument, ProductResult>()
      .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => Guid.Parse(src.Id)))
      .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Name))
      .ForMember(dest => dest.ProductDescription, opt => opt.MapFrom(src => src.Description))
      .ForMember(dest => dest.PriceValueInCents, opt => opt.MapFrom(src => src.PriceValueInCents))
      .ForMember(dest => dest.StockQuantity, opt => opt.MapFrom(src => src.StockQuantity))
      .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags))
      .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories))
      .ForMember(dest => dest.SellerId, opt => opt.MapFrom(src => src.SellerId))
      .ForMember(dest => dest.TotalReviews, opt => opt.MapFrom(src => src.TotalReviews))
      .ForMember(dest => dest.Images, opt => opt.MapFrom(src => src.ProductImages))
      .ForMember(dest => dest.Promotion, opt => opt.MapFrom(src => src.Promotion))
      .ForMember(dest => dest.AverageRating, opt => opt.MapFrom(src => src.AverageRating));
  }

  public static List<string> ExtractImageUrls(ProductImages images)
  {
    return new[]
    {
      images.LeftImage?.Url,
      images.RightImage?.Url,
      images.FrontImage?.Url,
      images.BackImage?.Url,
      images.TopImage?.Url,
      images.BottomImage?.Url,
    }
      .OfType<string>() // filters out nulls and casts safely to string
      .Where(url => !string.IsNullOrWhiteSpace(url))
      .ToList();
  }
}
