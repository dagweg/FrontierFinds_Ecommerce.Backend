using Ecommerce.Application.UseCases.Orders.Commands.CreateOrder;
using FluentValidation;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
  public CreateOrderCommandValidator()
  {
    RuleFor(x => x.OrderProducts)
      .NotNull()
      .Must(x => x.Count() > 0)
      .WithMessage("At least one product is required.");

    RuleForEach(x => x.OrderProducts)
      .ChildRules(products =>
      {
        products
          .RuleFor(p => p.ProductId)
          .NotEmpty()
          .WithMessage("Product Id is required for each order product.");
        products
          .RuleFor(p => p.Quantity)
          .GreaterThan(0)
          .WithMessage("Quantity must be greater than 0 for each order product.");
      });

    RuleFor(x => x.BillingAddress)
      .NotNull()
      .WithMessage("Billing address is required.")
      .ChildRules(address =>
      {
        address.RuleFor(a => a.Street).NotEmpty().WithMessage("Billing street is required.");
        address.RuleFor(a => a.City).NotEmpty().WithMessage("Billing city is required.");
        address.RuleFor(a => a.State).NotEmpty().WithMessage("Billing state is required.");
        address.RuleFor(a => a.Country).NotEmpty().WithMessage("Billing country is required.");
        address.RuleFor(a => a.ZipCode).NotEmpty().WithMessage("Billing zip code is required.");
      });

    RuleFor(x => x.ShippingAddress)
      .NotNull()
      .WithMessage("Shipping address is required.")
      .ChildRules(address =>
      {
        address.RuleFor(a => a.Street).NotEmpty().WithMessage("Shipping street is required.");
        address.RuleFor(a => a.City).NotEmpty().WithMessage("Shipping city is required.");
        address.RuleFor(a => a.State).NotEmpty().WithMessage("Shipping state is required.");
        address.RuleFor(a => a.Country).NotEmpty().WithMessage("Shipping country is required.");
        address.RuleFor(a => a.ZipCode).NotEmpty().WithMessage("Shipping zip code is required.");
      });

    // RuleFor(x => x.PaymentInformation)
    //   .NotNull()
    //   .WithMessage("Payment information is required.")
    //   .ChildRules(payment =>
    //   {
    //     payment
    //       .RuleFor(p => p.CardHolderName)
    //       .NotEmpty()
    //       .WithMessage("Card holder name is required.");
    //     payment.RuleFor(p => p.CardNumber).NotEmpty().WithMessage("Card number is required.");
    //     payment
    //       .RuleFor(p => p.ExpiryMonth)
    //       .InclusiveBetween(1, 12)
    //       .WithMessage("Expiry month must be between 1 and 12.");
    //     payment
    //       .RuleFor(p => p.ExpiryYear)
    //       .GreaterThan(DateTime.Now.Year - 1)
    //       .WithMessage($"Expiry year must be greater than or equal to {DateTime.Now.Year}."); // Allowing previous year for edge cases, consider adjusting as needed
    //     payment.RuleFor(p => p.CVV).NotEmpty().WithMessage("CVV is required.");
    //   });
  }
}
