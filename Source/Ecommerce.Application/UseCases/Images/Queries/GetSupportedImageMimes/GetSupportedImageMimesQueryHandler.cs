using Ecommerce.Domain.Common.Entities;
using FluentResults;
using MediatR;

namespace Ecommerce.Application.UseCases.Images.Queries.GetSupportedImageMimes;

public class GetSupportedImageMimesQueryHandler
  : IRequestHandler<GetSupportedImageMimesQuery, Result<IEnumerable<string>>>
{
  public Task<Result<IEnumerable<string>>> Handle(
    GetSupportedImageMimesQuery request,
    CancellationToken cancellationToken
  )
  {
    return Task.FromResult(Image.SUPPORTED_FORMATS.Split(",").AsEnumerable().ToResult());
  }
}
