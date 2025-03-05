using Ecommerce.Application.Common.Interfaces.Persistence;
using Ecommerce.Application.Common.Utilities;
using Ecommerce.Domain.Common.ValueObjects;
using Ecommerce.Domain.OrderAggregate;
using Ecommerce.Domain.OrderAggregate.Entities;
using Ecommerce.Domain.OrderAggregate.ValueObjects;
using Ecommerce.Domain.ProductAggregate.ValueObjects;
using Ecommerce.Domain.UserAggregate.ValueObjects;
using FluentResults;
using MediatR;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Ecommerce.Application.UseCases.Orders.Commands.CreateOrder;

public class CreateOrderCommandHandler(
  IOrderRepository orderRepository,
  IProductRepository productRepository,
  IUnitOfWork unitOfWork
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

        var totalPrice = 0m;

        List<OrderItem> orderItems = productsFromDb
          .Select(p =>
          {
              totalPrice += p.Value.Price.Value; // accumulate totalPrice (OrderTotal)

              // p.Key  and the 2nd arg is the productId and quantity (respectively) w/c we precomputed previously
              return OrderItem.Create(p.Key, orderProductKeysWithQuantity[p.Key], p.Value.Price);
          })
          .ToList();

        OrderTotal orderTotal = OrderTotal.Create(totalPrice, currency: Price.BASE_CURRENCY);

        ShippingAddress shippingAddress = ShippingAddress.Create(
          street: request.ShippingAddress.Street,
          city: request.ShippingAddress.City,
          state: request.ShippingAddress.State,
          country: request.ShippingAddress.Country,
          zipCode: request.ShippingAddress.ZipCode
        );

        BillingAddress billingAddress = BillingAddress.Create(
          street: request.ShippingAddress.Street,
          city: request.ShippingAddress.City,
          state: request.ShippingAddress.State,
          country: request.ShippingAddress.Country,
          zipCode: request.ShippingAddress.ZipCode
        );

        PaymentInformation paymentInformation = PaymentInformation.Create(
          cardHolderName: Name.Create(request.PaymentInformation.CardHolderName),
          cardNumber: cardNumberResult.Value,
          expiryMonth: monthResult.Value,
          expiryYear: yearResult.Value,
          cvv: cvvResult.Value
        );

        // TODO: Make use of an external payment service (stripe, paypal ..)
        // var paymentResult = await paymentService.Process(...);


        var order = Order.Create(
          orderItems,
          orderTotal,
          shippingAddress,
          billingAddress,
          paymentInformation
        );

        await orderRepository.AddAsync(order);

        await unitOfWork.SaveChangesAsync();

        return Result.Ok();
    }
}
