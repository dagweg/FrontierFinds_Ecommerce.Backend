namespace Ecommerce.Application.Common.Errors;

public record ValidationError(string Path, string Message);
