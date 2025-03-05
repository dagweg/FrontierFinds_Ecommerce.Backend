using Ecommerce.Domain.Common.Errors;
using FluentResults;

namespace Ecommerce.Application.Common.Errors;

public class UploadError(string Path, string Message)
  : FluentErrorBase(Path, Message, "Failed trying to upload the file.")
{
  public static Result GetResult(string path, string message)
  {
    return Result.Fail(new UploadError(path, message));
  }
}
