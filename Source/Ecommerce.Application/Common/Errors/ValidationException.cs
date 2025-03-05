namespace Ecommerce.Application.Common.Errors;

public class ValidationException : Exception
{
    public IReadOnlyList<ValidationError> Errors { get; set; }

    public ValidationException(IList<ValidationError> errors)
    {
        Errors = errors.AsReadOnly();
    }
}
