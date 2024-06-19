using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Text.Json;

namespace BuildingBlocks.Core.Behaviors;

public sealed class LoggingBehavior<TRequest, TResponse>
      (ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull, IRequest<TResponse>
    where TResponse : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        logger.LogInformation("[START] Handler Request={Request} | Response={Response} | Request Data={request}",
            typeof(TRequest).Name, typeof(TResponse).Name, JsonSerializer.Serialize(request));

        var time = new Stopwatch();
        time.Start();

        var response = await next();

        time.Stop();

        var timeTaken = time.Elapsed;
        if (timeTaken.Seconds > 3)
            logger.LogWarning("[PERFORMANCE] The request {Request} took {timeTaken}", typeof(TRequest).Name, timeTaken);

        logger.LogInformation("[START] Handler Request={Request} | Response={Response} | Response Data={request}", 
            typeof(TRequest).Name,typeof(TResponse).Name, JsonSerializer.Serialize(response));

        return response;
    }
}
