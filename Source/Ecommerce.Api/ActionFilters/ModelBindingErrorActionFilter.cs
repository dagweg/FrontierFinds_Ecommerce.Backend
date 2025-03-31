using Ecommerce.Application.Common.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Ecommerce.Api.ActionFilters;

public class ModelBindingErrorActionFilter : IActionFilter
{
  public void OnActionExecuted(ActionExecutedContext context) { }

  public void OnActionExecuting(ActionExecutingContext context)
  {
    // Intercept the model state errors
    if (!context.ModelState.IsValid)
    {
      var errors = context
        .ModelState.Where(ms => ms.Value is not null && ms.Value.Errors.Any())
        .ToDictionary(
          kvp => kvp.Key.Replace("$.", ""),
          kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
        );

      var response = new ProblemDetails
      {
        Title = "One or more validation errors occurred.",
        Status = StatusCodes.Status400BadRequest,
        Detail = "The request contains invalid data, please correct the errors and try again.",
        Instance = context.HttpContext.Request.Path,
        Extensions = { ["errors"] = errors },
      };

      LogPretty.Log(errors);
      context.Result = new JsonResult(response) { StatusCode = StatusCodes.Status400BadRequest };
    }
  }
}
