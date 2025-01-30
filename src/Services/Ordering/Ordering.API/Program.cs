using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;
using Ordering.Infrastructure.Data.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
        .AddApplicationServices(builder.Configuration)
        .AddInfrastructureServices(builder.Configuration, builder.Environment)
        .AddApiServices();

var app = builder.Build();

app.UseApiServices();

await app.CheckInitializeDatabaseAsync();

app.Run();
