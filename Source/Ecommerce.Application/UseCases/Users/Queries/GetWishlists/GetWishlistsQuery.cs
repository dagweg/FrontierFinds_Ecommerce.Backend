using Ecommerce.Application.Common.Models;
using Ecommerce.Application.UseCases.Common.Interfaces;
using Ecommerce.Application.UseCases.Products.Common;
using Ecommerce.Application.UseCases.Users.Common;
using FluentResults;
using MediatR;

namespace Ecommerce.Application.UseCases.Users.Queries.GetCartItems;

public record GetWishlistsQuery : PaginationParametersImmutable, IRequest<Result<WishlistsResult>>;
