using FluentResults;
using MediatR;

namespace Ecommerce.Application.UseCases.Users.Commands.ClearCart;

public record ClearCartCommand : IRequest<Result>;
