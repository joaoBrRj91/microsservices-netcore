using BuildingBlocks.Core.Configurations;
using Carter;

namespace Ordering.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        //services.AddCarter(
        //                 typeof(Program).Assembly,
        //                 typeof(CreateOrderEndpoint),
        //                 typeof(DeleteOrderEndpoint),
        //                 typeof(UpdateOrderEndpoint),
        //                 typeof(GetOrdersEndpoint),
        //                 typeof(GetOrdersByNameEndpoint),
        //                 typeof(GetOrdersByCustomerEndpoint));
        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        app.UseCommonApiServices();
        return app;
    }
}
