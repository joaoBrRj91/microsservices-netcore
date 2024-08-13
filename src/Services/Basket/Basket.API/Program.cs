using Basket.API.Configurations;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

#region Add Services To The Container - DI
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfraServices(builder.Configuration);

builder.Services.AddBusinessServices();

var app = builder.Build();

#endregion

#region Configure The HTTP Request Pipeline - Middleware

app.UseInfraServices();

app.Run();

#endregion