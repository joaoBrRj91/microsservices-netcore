using BuildingBlocks.Messaging.MassTransit;
using Discount.Grpc;

namespace Basket.API.Configurations
{
    public static class DependencyAddSpecificServices
    {
        public static void AddInfraServices(this IServiceCollection services, IConfiguration configuration)
        {
            // API
            services.AddCarterService(
                         typeof(Program).Assembly,
                         typeof(GetBasketEndpoint),
                         typeof(StoreBasketEndpoint),
                         typeof(DeleteBasketEndpoint));

            // Mediator - CQRS
            services.AddMediatorWithFluentValidatorServices(typeof(Program).Assembly);

            // Global Excpetion
            services.AddGlobalExceptionHandler();

            // Marten Db - Transactional document db
            services.AddMarten(options =>
            {
                options.Connection(configuration.GetConnectionString("DefaultConnection")!);
                options.Schema.For<ShoppingCart>().Identity(x => x.UserName);

            }).UseLightweightSessions();


            // Health Ckecks
            services.AddHealthChecks()
                .AddNpgSql(configuration.GetConnectionString("DefaultConnection")!)
                .AddRedis(configuration.GetConnectionString("Redis")!);


            // Redis - Distrubuted Cache
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("Redis");
                //options.InstanceName = "Basket";
            });


            // Grpc - External comunication api
            services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(options =>
            {
                options.Address = new Uri(configuration["GrpcSettings:DiscountUrl"]!);
            })
                .ConfigurePrimaryHttpMessageHandler(() =>
                {
                    return new HttpClientHandler
                    {
                        ServerCertificateCustomValidationCallback =
                        HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                    };
                });

            // Queue -  Async communication
            services.AddMessageBroker(configuration);
        }

        public static void AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.Decorate<IBasketRepository, CacheBasketRepository>();
        }
    }
}
