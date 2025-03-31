using Ecommerce.Application.Common.Models;
using Ecommerce.Application.UseCases.Orders.Common;
using FluentResults;
using MediatR;

namespace Ecommerce.Application.UseCases.Orders.Queries.GetOrders;

public record GetOrdersCommand() : PaginationParametersImmutable, IRequest<Result<OrdersResult>>;
