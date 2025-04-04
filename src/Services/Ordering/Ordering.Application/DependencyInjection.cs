﻿using BuildingBlocks.Core.Configurations;
using BuildingBlocks.Messaging.MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;
using System.Reflection;

namespace Ordering.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddMediatorWithFluentValidatorServices(Assembly.GetExecutingAssembly(), false);
        services.AddFeatureManagement();
        services.AddMessageBroker(configuration, Assembly.GetExecutingAssembly());
        return services;
    }
}
