using FluentResults;
using MediatR;

namespace Ecommerce.Application.UseCases.Images.Queries.GetSupportedImageMimes;

public record GetSupportedImageMimesQuery : IRequest<Result<IEnumerable<string>>>;
