using AutoMapper;
using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.Common.Interfaces.Providers.Context;
using Ecommerce.Application.Common.Interfaces.Providers.Payment.Stripe;
using Ecommerce.Application.Common.Utilities;
using Ecommerce.Domain.Common.ValueObjects;
using Ecommerce.Domain.OrderAggregate;
using Ecommerce.Domain.OrderAggregate.Entities;
using Ecommerce.Domain.OrderAggregate.ValueObjects;
using Ecommerce.Domain.PaymentAggregate.Enums;
using Ecommerce.Domain.ProductAggregate.Entities;
using Ecommerce.Domain.ProductAggregate.ValueObjects;
using Ecommerce.Domain.UserAggregate.ValueObjects;
using FluentResults;
using MediatR;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Ecommerce.Application.UseCases.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler(
  IUserContextService userContext,
  IOrderRepository orderRepository,
  IProductRepository productRepository,
  IUnitOfWork unitOfWork,
  IMapper mapper,
  IStripeService stripeService
) : IRequestHandler<CreateOrderCommand, Result<CreateCheckoutSessionResult>>
{
  public async Task<Result<CreateCheckoutSessionResult>> Handle(
    CreateOrderCommand request,
    CancellationToken cancellationToken
  )
  {
    var userId = userContext.GetValidUserId();
    if (userId.IsFailed)
      return userId.ToResult();

    // create value objects
    // var cardNumberResult = CardNumber.Create(request.PaymentInformation.CardNumber);
    // var monthResult = Month.Create(request.PaymentInformation.ExpiryMonth);
    // var yearResult = Year.Create(request.PaymentInformation.ExpiryYear);
    // var cvvResult = CVV.Create(request.PaymentInformation.CVV);

    // validate
    // if (cardNumberResult.IsFailed)
    //   return cardNumberResult.ToResult();
    // if (monthResult.IsFailed)
    //   return monthResult.ToResult();
    // if (yearResult.IsFailed)
    //   return yearResult.ToResult();
    // if (cvvResult.IsFailed)
    //   return cvvResult.ToResult();

    var orderProductKeysWithQuantity = new Dictionary<ProductId, int>();
    foreach (var orderProduct in request.OrderProducts)
    {
      var productIdResult = ConversionUtility.ToGuid(orderProduct.ProductId);
      if (productIdResult.IsFailed)
        return productIdResult.ToResult();

      orderProductKeysWithQuantity.Add(
        ProductId.Convert(productIdResult.Value),
        orderProduct.Quantity
      );
    }

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

    var order = Order.Create(userId.Value, orderItems, orderTotal, shippingAddress, billingAddress);

    // TODO: Make use of an external payment service (stripe, paypal ..)
    // var paymentResult = await paymentService.Process(...);


    List<string> GetProductImagesAsList(ProductImages productImages)
    {
      var images = new List<string>();

      if (productImages.Thumbnail is not null)
        images.Add(productImages.Thumbnail.Url);

      if (productImages.LeftImage is not null)
        images.Add(productImages.LeftImage.Url);

      if (productImages.RightImage is not null)
        images.Add(productImages.RightImage.Url);

      if (productImages.BackImage is not null)
        images.Add(productImages.BackImage.Url);

      if (productImages.FrontImage is not null)
        images.Add(productImages.FrontImage.Url);

      if (productImages.TopImage is not null)
        images.Add(productImages.TopImage.Url);

      if (productImages.BottomImage is not null)
        images.Add(productImages.BottomImage.Url);

      return images;
    }

    var result = await stripeService.CreateCheckoutSessionAsync(
      new CheckoutSessionRequest
      {
        lineItems = orderItems
          .Select(i => new OrderItemCheckout(
            productsFromDb[i.ProductId].Name,
            productsFromDb[i.ProductId].Description,
            GetProductImagesAsList(productsFromDb[i.ProductId].Images),
            i.Quantity,
            i.Price.ValueInCents
          ))
          .ToList(),
        paymentMethod = PaymentMethod.CreditCard,
        paymentMode = PaymentMode.Payment, // single payment (not subscription)
      }
    );

    if (result.IsFailed)
      return result.ToResult();

    await orderRepository.AddAsync(order);

    await unitOfWork.SaveChangesAsync();

    return result;
  }
}
