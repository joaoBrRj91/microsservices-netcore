using Catalog.API.Products.CreateProduct;

namespace Catalog.API.Configurations
{
    public static class DependencyInjection
    {
        public static void AddBuidingBlockServices(this IServiceCollection services, IConfiguration configuration)
        {
            //Register Carter with assembly have CarterModules 
            services.AddCarter(new DependencyContextAssemblyCatalog(typeof(Program).Assembly), config =>
            {
                config.WithModule<CreateProductEndpoint>();
            });


            //Register Mediator
            services.AddMediatR(config =>
            {
                //Tell Mediator when the commands and handlers is registrated
                config.RegisterServicesFromAssembly(typeof(Program).Assembly);
            });


            //Register Marter with no cache and no proxy for entities for performance
            services.AddMarten(config =>
            {
                config.Connection(configuration.GetConnectionString("DefaultConnection")!);
                //For Prodution
               // config.AutoCreateSchemaObjects = Weasel.Core.AutoCreate.None;
            }).UseLightweightSessions();
        }
    }
}
