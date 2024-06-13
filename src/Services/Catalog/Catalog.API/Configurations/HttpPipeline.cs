using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Configurations
{
    public static class HttpPipeline
    {
        public static void AddExceptionHandler(this IApplicationBuilder application)
        {
            application.UseExceptionHandler(expectionHandlerApp =>
            {
                expectionHandlerApp.Run(async context =>
                {
                    var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

                    if (exception is null)
                        return;

                    var problemDetail = new ProblemDetails
                    {
                        Title = exception.Message,
                        Status = StatusCodes.Status500InternalServerError,
                        Detail = "Contact the product registration administrator via email at contact.eshop.products@eshop.com"
                    };

                    var logger = context.RequestServices.GetService<ILogger<Program>>()!;
                    logger.LogError(exception, exception.Message);

                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    context.Response.ContentType = "application/problem+json";

                    await context.Response.WriteAsJsonAsync(problemDetail);
                });

            });
        }
    }
}
