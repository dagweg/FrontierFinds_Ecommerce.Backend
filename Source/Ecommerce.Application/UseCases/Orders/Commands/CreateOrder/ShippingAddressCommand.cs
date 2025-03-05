namespace Ecommerce.Application.UseCases.Orders.Commands.CreateOrder;

public class ShippingAddressCommand
{
    public required string Street { get; set; }
    public required string City { get; set; }
    public required string State { get; set; }
    public required string Country { get; set; }
    public required string ZipCode { get; set; }
}
