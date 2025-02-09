using FluentResults;
using MediatR;

namespace Ecommerce.Application.UseCases.Products.Commands.DeleteProduct;

public record DeleteProductCommand(string ProductId) : IRequest<Result>;
