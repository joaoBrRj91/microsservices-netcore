using BuildingBlocks.Core.Configurations;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Ordering.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatorWithFluentValidatorServices(Assembly.GetExecutingAssembly());
        return services;
    }
}
