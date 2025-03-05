using Ecommerce.Application.Common.Errors;
using Ecommerce.Application.Common.Utilities;
using Ecommerce.Application.UseCases.Users.Common;
using Ecommerce.Domain.Common.Errors;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Ecommerce.Api.ActionFilters;

public class FluentResultsActionFilter : IActionFilter
{
    public void OnActionExecuted(ActionExecutedContext context)
    {
        var result = context.Result;

        if (result is ObjectResult objectResult)
        {
            if (objectResult.Value is FluentResults.IResultBase fluentResult)
            {
                // Handle the failure
                var error = fluentResult.Errors.First();
                var errorName = error.GetType().Name;
                var problemDetails = new ProblemDetails
                {
                    Title = ConversionUtility.PascalToSpacedPascal(errorName),
                    Detail = error.Message,
                    Status = StatusCodes.Status500InternalServerError,
                };

                if (error is FluentErrorBase fluentError)
                {
                    problemDetails.Detail = fluentError.Detail;
                    problemDetails.Extensions.Add("path", fluentError.Path);
                    problemDetails.Extensions.Add("message", fluentError.Message);

                    int status = StatusCodes.Status500InternalServerError;

                    switch (fluentError)
                    {
                        case AuthenticationError:
                            status = StatusCodes.Status401Unauthorized;
                            break;
                        case ValidationError:
                        case LengthError:
                        case BelowZeroError:
                        case InvalidCurrencyError:
                        case InvalidDateRangeError:
                        case PasswordMatchError:
                        case IncorrectCurrentPasswordError:
                        case ExpiryError:
                            status = StatusCodes.Status400BadRequest;
                            break;
                        case AlreadyExistsError:
                            status = StatusCodes.Status409Conflict;
                            break;
                        case NotFoundError:
                            status = StatusCodes.Status404NotFound;
                            break;
                        default:
                            status = StatusCodes.Status500InternalServerError;
                            break;
                    }

                    problemDetails.Status = status;
                }

                context.Result = new ObjectResult(problemDetails) { StatusCode = problemDetails.Status };
            }
            else
            {
                Console.WriteLine("Result is not a failed Result object");
            }
        }
    }

    public void OnActionExecuting(ActionExecutingContext context) { }
}
