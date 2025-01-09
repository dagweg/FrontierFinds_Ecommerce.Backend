namespace Ecommerce.Application.Common.Errors;

public class AlreadyExistsError(string path, string message)
  : FluentErrorBase(path, message, $"A resource already exists.");
