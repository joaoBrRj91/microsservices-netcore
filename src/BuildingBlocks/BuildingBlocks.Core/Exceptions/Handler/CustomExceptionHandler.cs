using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Core.Exceptions.Handler;

public sealed class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError("Error message: {Message}, Time of occurrence {UtcNow}", exception.Message, DateTime.UtcNow);

        SetStatusCodeResponse(exception, context);

        var problemDetails = GenerateBaseProblemDetailsData(exception, context);

        problemDetails.Status = context.Response.StatusCode;

        await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);
        return true;
    }

    private static ProblemDetails GenerateBaseProblemDetailsData(Exception exception, HttpContext context)
    {
        var problemDetails = new ProblemDetails
        {
            Title = exception.GetType().Name,
            Detail = exception.Message,
            Instance = context.Request.Path
        };

        problemDetails.Extensions.Add("tradeId", context.TraceIdentifier);

        if (exception is ValidationException validationException)
            problemDetails.Extensions.Add("ValidationErrors", validationException.Errors);

        return problemDetails;

    }

    private static void SetStatusCodeResponse(Exception exception, HttpContext context)
    {
        _ = exception switch
        {
            InternalServerErrorException => context.Response.StatusCode = StatusCodes.Status500InternalServerError,
            ValidationException or BadRequestException => context.Response.StatusCode = StatusCodes.Status400BadRequest,
            NotFoundException => context.Response.StatusCode = StatusCodes.Status404NotFound,
            _ => context.Response.StatusCode = StatusCodes.Status500InternalServerError

        };
    }

}
