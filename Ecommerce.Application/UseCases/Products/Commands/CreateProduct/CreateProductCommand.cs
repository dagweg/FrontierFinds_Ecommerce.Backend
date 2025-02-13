using Ecommerce.Application.UseCases.Images.CreateImage;
using Ecommerce.Application.UseCases.Products.Common;
using FluentResults;
using MediatR;

namespace Ecommerce.Application.UseCases.Products.CreateUser.Commands;

public record CreateProductCommand(
  string ProductName,
  string ProductDescription,
  int StockQuantity,
  decimal PriceValue,
  string PriceCurrency,
  CreateImageCommand Thumbnail,
  CreateImageCommand? LeftImage = null,
  CreateImageCommand? RightImage = null,
  CreateImageCommand? FrontImage = null,
  CreateImageCommand? BackImage = null,
  List<string>? Tags = null
) : IRequest<Result<ProductResult>>;
