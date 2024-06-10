using Catalog.API.Configurations;

#region Add Services To The Container - DI

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddBuidingBlockServices(builder.Configuration);

#endregion


#region Configure The HTTP Request Pipeline - Middleware
var app = builder.Build();

/*Find all class is have implementation ICarterModule (in assembly reigistrated) 
 * and map endpoints defined called AddRouter Method implemented from Interface*/
app.MapCarter();

app.AddGlobalExceptionHandler();

app.Run();
#endregion
