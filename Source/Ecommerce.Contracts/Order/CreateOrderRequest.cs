using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Contracts.Order;

public class CreateOrderRequest
{
  [Required]
  [MinLength(1)]
  public IEnumerable<OrderProductRequest> Products = null!;

  [Required]
  public ShippingAddressRequest ShippingAddress { get; set; } = null!;

  [Required]
  public BillingAddressRequest BillingAddress { get; set; } = null!;

  [Required]
  public PaymentInformationRequest PaymentInformation { get; set; } = null!;
}
