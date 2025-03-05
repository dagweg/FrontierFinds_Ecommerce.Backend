using System.ComponentModel.DataAnnotations;
using Ecommerce.Api.Attributes;

namespace Ecommerce.Contracts.Cart;

public class AddCartItemRequest
{
    [Required]
    [Guid]
    public string ProductId { get; set; } = null!;

    [Required]
    [Range(1, int.MaxValue)]
    public int Quantity { get; set; }
}
