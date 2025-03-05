using FluentResults;
using MediatR;

namespace Ecommerce.Application.UseCases.Users.Commands.WishlistProducts;

public record WishlistProductsCommand : IRequest<Result>
{
    public required List<string> ProductIds;
}
