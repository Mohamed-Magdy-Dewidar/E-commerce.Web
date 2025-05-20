using System.Text.Json;
using DomainLayer.Exceptions;
using Shared.ErrorModels;

namespace E_commerce.Web.CustomMiddleWares
{
    public class CustomExceptionHandlerMiddleWares
    {
         private readonly RequestDelegate _next;
         private readonly ILogger<CustomExceptionHandlerMiddleWares> _logger; 
        public CustomExceptionHandlerMiddleWares(RequestDelegate Next , ILogger<CustomExceptionHandlerMiddleWares> logger)
        {

            _next = Next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {

                await _next.Invoke(context);

                await HandleNotFoundEndPiontAsync(context);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, message: ex.Message);
                // Create a Json Object contianing the error message and status Code and Send it to the Client

                await HandleExceptionAsync(context, ex);

            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception ex)
        {


            var ResponseToReturn = new ErrorToReturn()
            {
                StatusCode = context.Response.StatusCode,
                ErrorMessage = ex.Message
            };

            context.Response.StatusCode = ex switch
            {
                BadRequestException badRequestException => GetBadRequestException(badRequestException , ResponseToReturn),
                UnauthorizedException => StatusCodes.Status401Unauthorized,
                NotFoundException => StatusCodes.Status404NotFound,
                _ => StatusCodes.Status500InternalServerError
            };


            //JsonSerializer.Serialize(ResponseToReturn);
            // return Object as Json
            await context.Response.WriteAsJsonAsync(ResponseToReturn);
        }

        private static int GetBadRequestException(BadRequestException badRequestException, ErrorToReturn responseToReturn)
        {
            responseToReturn.Errors = badRequestException.Errors;
            return StatusCodes.Status400BadRequest; 
        }

        private static async Task HandleNotFoundEndPiontAsync(HttpContext context)
        {
            if (context.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                context.Response.ContentType = "application/json";
                var ResponseToReturn = new ErrorToReturn()
                {
                    StatusCode = context.Response.StatusCode,
                    ErrorMessage = $"End Piont {context.Request.Path} was not found"
                };
                await context.Response.WriteAsJsonAsync(ResponseToReturn);
            }
        }
    }



}
