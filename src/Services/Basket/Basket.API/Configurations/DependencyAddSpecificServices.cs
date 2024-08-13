namespace Basket.API.Configurations
{
    public static class DependencyAddSpecificServices
    {
        public static void AddInfraServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCarterService(
                         typeof(Program).Assembly,
                         typeof(GetBasketEndpoint),
                         typeof(StoreBasketEndpoint),
                         typeof(DeleteBasketEndpoint));

            services.AddMediatorWithFluentValidatorServices(typeof(Program).Assembly);

            services.AddGlobalExceptionHandler();

            services.AddMarten(options =>
            {
                options.Connection(configuration.GetConnectionString("DefaultConnection")!);
                options.Schema.For<ShoppingCart>().Identity(x => x.UserName);

            }).UseLightweightSessions();


            services.AddHealthChecks()
                .AddNpgSql(configuration.GetConnectionString("DefaultConnection")!)
                .AddRedis(configuration.GetConnectionString("Redis")!);

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("Redis");
                //options.InstanceName = "Basket";
            });
        }

        public static void AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.Decorate<IBasketRepository, CacheBasketRepository>();
        }
    }
}
