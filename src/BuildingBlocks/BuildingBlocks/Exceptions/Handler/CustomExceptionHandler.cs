using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace BuildingBlocks.Exceptions.Handler
{
    public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger)
        : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
        {
            logger.LogError(exception, "An unhandled exception occurred while processing the request.");
            (string Detail, int StatusCode, string Title) details = exception switch
            {
                InternalServerException =>
                (
                    exception.Message,
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError,
                    exception.GetType().Name
                ),
                ValidationException =>
                (
                    exception.Message,
                    context.Response.StatusCode = StatusCodes.Status400BadRequest,
                    exception.GetType().Name
                ),
                BadRequestException =>
                (
                    exception.Message,
                    context.Response.StatusCode = StatusCodes.Status400BadRequest,
                    exception.GetType().Name
                ),
                NotFoundException =>
                (
                    exception.Message,
                    context.Response.StatusCode = StatusCodes.Status404NotFound,
                    exception.GetType().Name
                ),
                _ =>
                (
                    exception.Message,
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError,
                    exception.GetType().Name
                )
            };
            var problemDetails = new ProblemDetails
            {
                Title = details.Title,
                Detail = details.Detail,
                Status = details.StatusCode,
                Instance = context.Request.Path
            };
            problemDetails.Extensions.Add("traceId", context.TraceIdentifier);
            if(exception is ValidationException validationException)
            {
                problemDetails.Extensions.Add("ValidationErrors", validationException.ValidationResult);
            }
            await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
            return true;
        }
    }
}
