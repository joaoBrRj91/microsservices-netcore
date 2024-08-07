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


builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Services.Decorate<IBasketRepository, CacheBasketRepository>();
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
   //options.InstanceName = "Basket";
});


var app = builder.Build();

#endregion

#region Configure The HTTP Request Pipeline - Middleware

//Use Common Services Added In Container -DI
app.UseCommonServices();

app.Run();

#endregion