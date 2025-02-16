using FluentResults;
using MediatR;

namespace Ecommerce.Application.UseCases.Images.Commands.DeleteImage;

public sealed record DeleteImageCommand(string ObjectIdentifier) : IRequest<Result>;
