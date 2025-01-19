using AutoMapper;
using Ecommerce.Application.UseCases.Images.Common;
using Ecommerce.Application.UseCases.Products.Common;
using Ecommerce.Domain.Common.Entities;
using Ecommerce.Domain.Common.ValueObjects;
using FluentResults;
using MediatR;

namespace Ecommerce.Application.UseCases.Products.Commands;

public class CreateProductCommandHandler
  : IRequestHandler<CreateProductCommand, Result<ProductResult>>
{
  private readonly IMapper _mapper;

  public CreateProductCommandHandler(IMapper mapper)
  {
    _mapper = mapper;
  }

  public Task<Result<ProductResult>> Handle(
    CreateProductCommand command,
    CancellationToken cancellationToken
  )
  {
    try
    {
      var thumb = _mapper.Map<ImageResult>(command.Thumbnail);
      thumb.Url = "https://aws.s3.com/1.jpg";

      var images = new ProductImagesResult();

      var currency = Price.ToCurrency(command.PriceCurrency);

      // var product = Product.Create(ProductName.Create(command.ProductName), ProductDescription.Create(command.ProductDescription), Price.Create(command.PriceValue, command.PriceCurrency)
      var product = new ProductResult
      {
        ProductId = "1",
        ProductName = command.ProductName,
        ProductDescription = command.ProductDescription,
        StockQuantity = command.StockQuantity,
        PriceValue = command.PriceValue,
        PriceCurrency = command.PriceCurrency,
        Thumbnail = thumb,
        Images = images,
      };

      return Task.FromResult(Result.Ok(product));
    }
    catch (Exception ex)
    {
      Console.WriteLine(ex);
      throw;
    }
  }
}
