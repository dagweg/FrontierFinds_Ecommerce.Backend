using FluentResults;

namespace Ecommerce.Application.Common.Errors;

public class FluentErrorBase : IError
{
  public List<IError> Reasons { get; private set; }

  public string Message { get; init; }
  public string Detail { get; init; }
  public string Path { get; init; }

  public Dictionary<string, object> Metadata { get; init; }

  public FluentErrorBase(string path, string message, string detail)
  {
    Message = message;
    Path = path;
    Reasons = [];
    Metadata = [];
    Detail = detail;
  }
}
