#region Add Services To The Container - DI

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarterService(typeof(Program).Assembly, typeof(GetBasketEndpoint), typeof(StoreBasketEndpoint), typeof(DeleteBasketEndpoint));

builder.Services.AddMediatorWithFluentValidatorServices(typeof(Program).Assembly);

builder.Services.AddGlobalExceptionHandler();

builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("DefaultConnection")!);
    options.Schema.For<ShoppingCart>().Identity(x => x.UserName);

}).UseLightweightSessions();

var app = builder.Build();

#endregion

#region Configure The HTTP Request Pipeline - Middleware

/*Find all class is have implementation ICarterModule (in assembly reigistrated) 
 * and map endpoints defined called AddRouter Method implemented from Interface*/
app.MapCarter();



//Global Exception handler defined in BuildingBlocks for all microsservices
app.UseExceptionHandler(options => { });

app.Run();

#endregion