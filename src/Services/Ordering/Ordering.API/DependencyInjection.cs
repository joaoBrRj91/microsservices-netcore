using BuildingBlocks.Core.Configurations;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Ordering.API.Endpoints;

namespace Ordering.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddCarterService(
                         typeof(Program).Assembly,
                         typeof(CreateOrderEndpoint),
                         typeof(DeleteOrderEndpoint),
                         typeof(UpdateOrderEndpoint),
                         typeof(GetOrdersEndpoint),
                         typeof(GetOrdersByNameEndpoint),
                         typeof(GetOrdersByCustomerEndpoint));

        services.AddGlobalExceptionHandler();
        services.AddHealthChecks();
        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        app.UseCommonApiServices(isExceptionHandlerEnable: true);

        app.UseHealthChecks("/health",
             new HealthCheckOptions
             {
                 ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
             });

        return app;
    }
}
