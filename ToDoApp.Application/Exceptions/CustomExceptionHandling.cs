using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ToDoApp.Application.Exceptions;

public class CustomExceptionHandling : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var problemDetail = new ProblemDetails()
        {
            Title = exception.Message,
            Status = StatusCodes.Status500InternalServerError,
            Detail = exception.StackTrace,
            Instance = httpContext.Request.Path
        };
        problemDetail.Extensions.Add("RequestId", httpContext.TraceIdentifier);
        if (exception.InnerException is ValidationException validationException)
        {
            problemDetail.Extensions.Add("ValidationErrors", validationException.Errors);
        }

        if (exception is NotFoundException notFoundException)
        {
            problemDetail.Status = StatusCodes.Status404NotFound;
            httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
        }
        httpContext.Response.WriteAsJsonAsync(problemDetail);
        return true;
    }
}