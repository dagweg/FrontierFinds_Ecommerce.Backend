using AutoMapper;
using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.Common.Interfaces.Providers.Payment.Stripe;
using Ecommerce.Application.Common.Utilities;
using Ecommerce.Domain.Common.ValueObjects;
using Ecommerce.Domain.OrderAggregate;
using Ecommerce.Domain.OrderAggregate.Entities;
using Ecommerce.Domain.OrderAggregate.ValueObjects;
using Ecommerce.Domain.PaymentAggregate.Enums;
using Ecommerce.Domain.ProductAggregate.ValueObjects;
using Ecommerce.Domain.UserAggregate.ValueObjects;
using FluentResults;
using MediatR;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Ecommerce.Application.UseCases.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler(
  IOrderRepository orderRepository,
  IProductRepository productRepository,
  IUnitOfWork unitOfWork,
  IMapper mapper,
  IStripeService stripeService
) : IRequestHandler<CreateOrderCommand, Result>
{
  public async Task<Result> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
  {
    // create value objects
    var cardNumberResult = CardNumber.Create(request.PaymentInformation.CardNumber);
    var monthResult = Month.Create(request.PaymentInformation.ExpiryMonth);
    var yearResult = Year.Create(request.PaymentInformation.ExpiryYear);
    var cvvResult = CVV.Create(request.PaymentInformation.CVV);

    // validate
    if (cardNumberResult.IsFailed)
      return cardNumberResult.ToResult();
    if (monthResult.IsFailed)
      return monthResult.ToResult();
    if (yearResult.IsFailed)
      return yearResult.ToResult();
    if (cvvResult.IsFailed)
      return cvvResult.ToResult();

    // create a dict with <productId, quantity>
    var orderProductKeysWithQuantity = request.OrderProducts.ToDictionary(
      x => ProductId.Convert(ConversionUtility.ToGuid(x.ProductId).Value),
      x => x.Quantity
    );

    // fetch all products from db by passing Enumerable<ProductIds>
    var productsFromDb = await productRepository.BulkGetByIdAsync(
      orderProductKeysWithQuantity.Select(kvp => kvp.Key)
    );

    long totalAmountInCents = 0L;

    List<OrderItem> orderItems = productsFromDb
      .Select(p =>
      {
        totalAmountInCents += p.Value.Price.ValueInCents; // accumulate totalPrice (OrderTotal)

        // p.Key  and the 2nd arg is the productId:quantity  w/c we precomputed previously
        return OrderItem.Create(p.Key, orderProductKeysWithQuantity[p.Key], p.Value.Price);
      })
      .ToList();

    OrderTotal orderTotal = OrderTotal.Create(totalAmountInCents, currency: Price.BASE_CURRENCY);

    ShippingAddress shippingAddress = mapper.Map<ShippingAddress>(request.ShippingAddress);

    BillingAddress billingAddress = mapper.Map<BillingAddress>(request.BillingAddress);

    var order = Order.Create(orderItems, orderTotal, shippingAddress, billingAddress);

    // TODO: Make use of an external payment service (stripe, paypal ..)
    // var paymentResult = await paymentService.Process(...);


    var result = await stripeService.CreateCheckoutSessionAsync(
      new CheckoutSessionRequest
      {
        lineItems = orderItems
          .Select(i => new OrderItemCheckout(
            productsFromDb[i.ProductId].Name,
            i.Quantity,
            i.Price.ValueInCents
          ))
          .ToList(),
        paymentMethod = PaymentMethod.CreditCard,
        paymentMode = PaymentMode.Payment, // single payment (not subscription)
      }
    );

    await orderRepository.AddAsync(order);

    await unitOfWork.SaveChangesAsync();

    return Result.Ok(result).ToResult();
  }
}
