using System.ComponentModel.DataAnnotations;
using Ecommerce.Api.Attributes;

namespace Ecommerce.Contracts.Cart;

public class RemoveCartItemRequest
{
  [Required]
  [Guid]
  public string CartItemId { get; set; } = null!;
}
