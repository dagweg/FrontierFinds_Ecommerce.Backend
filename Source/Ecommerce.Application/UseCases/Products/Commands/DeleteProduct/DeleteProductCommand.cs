using FluentResults;
using MediatR;

namespace Ecommerce.Application.UseCases.Products.Commands.DeleteProduct;

public record DeleteProductCommand(List<string> productIds) : IRequest<Result>;
