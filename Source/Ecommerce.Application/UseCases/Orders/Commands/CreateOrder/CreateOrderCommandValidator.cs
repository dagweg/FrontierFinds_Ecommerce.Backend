using Ecommerce.Application.UseCases.Orders.Commands.CreateOrder;
using FluentValidation;

public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
{
  public CreateOrderCommandValidator()
  {
    RuleFor(x => x.OrderProducts).NotEmpty().NotNull().Must(x => x.Count() > 0);
  }
}
