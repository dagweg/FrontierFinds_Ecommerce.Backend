using Ecommerce.Application.Common.Models;
using Ecommerce.Application.UseCases.Orders.Common;
using Ecommerce.Domain.OrderAggregate;
using Ecommerce.Domain.ProductAggregate;

namespace Ecommerce.Application.Common.Models;

public class GetOrdersResult : GetResult<(Order order, IEnumerable<Product> products)> { }
