using Ecommerce.Application.UseCases.Common.Interfaces;
using Ecommerce.Application.UseCases.Products.Common;
using Ecommerce.Application.UseCases.Users.Common;
using FluentResults;
using MediatR;

namespace Ecommerce.Application.UseCases.Users.Queries.GetCartItems;

public record GetCartQuery(int pageNumber, int pageSize) : IRequest<Result<CartResult>>, IPaginated
{
    public int PageNumber { get; init; } = pageNumber;
    public int PageSize { get; init; } = pageSize;
}
