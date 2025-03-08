using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Ecommerce.Contracts.Order;

public record BillingAddressRequest(
  [Required] [NotNull] string Street,
  [Required] [NotNull] string City,
  [Required] [NotNull] string State,
  [Required] [NotNull] string Country,
  [Required] [NotNull] string ZipCode
);
