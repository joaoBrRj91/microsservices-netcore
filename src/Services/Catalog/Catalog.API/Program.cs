using Catalog.API.Configurations;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;


//Refactoring this program for the same rules and configuration business of the Basket.API Microsservies

#region Add Services To The Container - DI
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddBuidingBlockServices(builder.Configuration,builder.Environment);

var app = builder.Build();
#endregion


#region Configure The HTTP Request Pipeline - Middleware
/*Find all class is have implementation ICarterModule (in assembly reigistrated) 
 * and map endpoints defined called AddRouter Method implemented from Interface*/
app.MapCarter();

//This exception handler work only this microsservices Catalog. 
// app.AddExceptionHandler();

//Global Exception handler defined in BuildingBlocks for all microsservices
app.UseExceptionHandler(options => { });

app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });; 

app.Run();
#endregion
