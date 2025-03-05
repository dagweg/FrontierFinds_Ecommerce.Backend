using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Contracts.Order;

public class PaymentInformationRequest
{
    [Required]
    public string CardHolderName { get; set; } = null!;

    [Required]
    public string CardNumber { get; set; } = null!;

    [Required]
    public int ExpiryMonth { get; set; }

    [Required]
    public int ExpiryYear { get; set; }

    [Required]
    public string CVV { get; set; } = null!;
}
