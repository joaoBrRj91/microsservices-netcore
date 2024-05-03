using Catalog.API.Products.CreateProduct;

namespace Catalog.API.Configurations
{
    public static class DependencyInjection
    {
        public static void AddBuidingBlockServices(this IServiceCollection services)
        {
            //Register Carter with assembly have CarterModules 
            services.AddCarter(new DependencyContextAssemblyCatalog(typeof(Program).Assembly), c =>
            {
                c.WithModule<CreateProductEndpoint>();
            });

            services.AddMediatR(config =>
            {
                //Tell Mediator when the commands and handlers is registrated
                config.RegisterServicesFromAssembly(typeof(Program).Assembly);
            });

        }
    }
}
