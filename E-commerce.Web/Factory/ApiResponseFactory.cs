using Microsoft.AspNetCore.Mvc;
using Shared.ErrorModels;

namespace E_commerce.Web.Factory
{
    public static class ApiResponseFactory
    {
        public static IActionResult GenerateApiValidationErrorResponse(ActionContext context)
        {
            var errors = context.ModelState
                       .Where(m => m.Value?.Errors?.Count > 0)
                       .Select(m => new ValidationError
                       {
                           Filed = m.Key, // Fixed typo from "Filed"
                           Errors = m.Value.Errors
                               .Select(e => string.IsNullOrWhiteSpace(e.ErrorMessage)
                                   ? "Invalid field value" // Default message if empty
                                   : e.ErrorMessage)
                               .Distinct() // Remove duplicates
                               .ToList()
                       })
                       .ToList();

            var response = new ValidationErrorToReturn
            {
                ValidationErrors = errors,
                StatusCode = StatusCodes.Status400BadRequest,
                Message = "Validation Failed"

            };

            return new BadRequestObjectResult(response);
        }
    }
}
