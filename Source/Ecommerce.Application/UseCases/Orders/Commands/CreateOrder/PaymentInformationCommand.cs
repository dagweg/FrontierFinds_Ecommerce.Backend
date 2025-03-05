namespace Ecommerce.Application.UseCases.Orders.Commands.CreateOrder;

public class PaymentInformationCommand
{
    public required string CardHolderName { get; set; }
    public required string CardNumber { get; set; }
    public required int ExpiryMonth { get; set; }
    public required int ExpiryYear { get; set; }
    public required string CVV { get; set; }
}
