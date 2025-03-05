using FluentResults;
using MediatR;

namespace Ecommerce.Application.UseCases.Users.Commands.RemoveWishlistProducts;

public record RemoveWishlistProductsCommand : IRequest<Result>
{
    public required List<string> ProductIds;
}
