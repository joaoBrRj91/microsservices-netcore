﻿using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Ordering.Application.Data;
using Ordering.Infrastructure.Data.Interceptors;

namespace Ordering.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration, IWebHostEnvironment environment)
    {
        var connectionString = configuration.GetConnectionString("Database")!;

        services.AddDatabaseServices(connectionString, environment);
        services.AddHealthCheck(connectionString);

        return services;
    }

    private static void AddDatabaseServices(this IServiceCollection services,
        string connectionString, IWebHostEnvironment environment)
    {
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();

        services.AddDbContext<AppDbContext>((sp, opt) =>
        {
            opt.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
            opt.UseSqlServer(connectionString);
        });

        if (environment.IsDevelopment())
            services.AddScoped<AppDbContext>();

        services.AddScoped<IAppDbContext, AppDbContext>();

    }

    private static void AddHealthCheck(this IServiceCollection services,string connectionString)
    {
        services.AddHealthChecks()
              .AddSqlServer(connectionString);
    }
}
