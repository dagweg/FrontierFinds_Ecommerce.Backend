using AutoMapper;
using Ecommerce.Application.Common.Models;
using Ecommerce.Application.UseCases.Images.Common;
using Ecommerce.Application.UseCases.Orders.Commands.CreateOrder;
using Ecommerce.Application.UseCases.Orders.Common;
using Ecommerce.Application.UseCases.Products.Common;
using Ecommerce.Domain.OrderAggregate;
using Ecommerce.Domain.OrderAggregate.Entities;
using Ecommerce.Domain.OrderAggregate.ValueObjects;
using Ecommerce.Domain.ProductAggregate;
using Ecommerce.Domain.ProductAggregate.Entities;

namespace Ecommerce.Application.Common.Mapping;

public class OrderMappingProfile : Profile
{
  public OrderMappingProfile()
  {
    CreateMap<GetOrdersResult, OrdersResult>();

    CreateMap<Order, OrderResult>()
      .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Id.Value.ToString()))
      .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId.Value.ToString()))
      .ForMember(dest => dest.Total, opt => opt.MapFrom(src => src.Total))
      .ForMember(dest => dest.ShippingAddress, opt => opt.MapFrom(src => src.ShippingAddress))
      .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
      .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems.ToList()))
      .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderDate));

    CreateMap<ShippingAddressCommand, ShippingAddress>();

    CreateMap<OrderItem, OrderItemResult>()
      .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId.Value.ToString()))
      .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
      .ForMember(dest => dest.PriceValueInCents, opt => opt.MapFrom(src => src.Price.ValueInCents));

    // CreateMap<IReadOnlyList<OrderItem>, List<OrderItemResult>>(); // AutoMapper will use the OrderItem -> OrderItemResult mapping

    // Mapping for OrderTotal to OrderTotalResult
    CreateMap<OrderTotal, OrderTotalResult>()
      .ForMember(dest => dest.ValueTotalInCents, opt => opt.MapFrom(src => src.ValueTotalInCents))
      .ForMember(dest => dest.Currency, opt => opt.MapFrom(src => src.Currency.ToString()));

    // Mapping for Order to OrderResult
  }
}
