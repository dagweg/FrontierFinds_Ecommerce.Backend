using AutoMapper;
using Ecommerce.Application.UseCases.Images.Common;
using Ecommerce.Application.UseCases.Orders.Commands.CreateOrder;
using Ecommerce.Application.UseCases.Products.Common;
using Ecommerce.Domain.OrderAggregate.ValueObjects;
using Ecommerce.Domain.ProductAggregate;
using Ecommerce.Domain.ProductAggregate.Entities;

namespace Ecommerce.Application.Common.Mapping;

public class OrderMappingProfile : Profile
{
  public OrderMappingProfile()
  {
    CreateMap<BillingAddressCommand, BillingAddress>();
    CreateMap<ShippingAddressCommand, ShippingAddress>();
  }
}
