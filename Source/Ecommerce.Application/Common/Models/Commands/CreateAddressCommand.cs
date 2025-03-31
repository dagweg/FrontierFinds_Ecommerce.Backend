namespace Ecommerce.Application.Common.Models.Commands;

public class CreateAddressCommand
{
  public required string Street { get; set; }
  public required string City { get; set; }
  public required string State { get; set; }
  public required string Country { get; set; }
  public required string ZipCode { get; set; }
}
