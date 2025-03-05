using System.ComponentModel.DataAnnotations;
using Ecommerce.Api.Attributes;

namespace Ecommerce.Contracts.Order;

public record OrderProductRequest(
  [Required] [Guid] string ProductId,
  [Required] [Range(1, int.MaxValue)] int Quantity
);
