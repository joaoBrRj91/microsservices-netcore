﻿using BuildingBlocks.Core.Behaviors;
using Catalog.API.Products.CreateProduct;
using Catalog.API.Products.DeleteProduct;
using Catalog.API.Products.GetProductByCategory;
using Catalog.API.Products.GetProductById;
using Catalog.API.Products.GetProducts;
using Catalog.API.Products.UpdateProduct;

namespace Catalog.API.Configurations
{
    public static class DependencyInjection
    {
        public static void AddBuidingBlockServices(this IServiceCollection services, IConfiguration configuration)
        {
            var registrationFromAssembly = typeof(Program).Assembly;


            #region Register Carter with assembly have CarterModules 
            services.AddCarter(new DependencyContextAssemblyCatalog(registrationFromAssembly), config =>
            {
                config.WithModule<CreateProductEndpoint>();
                config.WithModule<DeleteProductEndpoint>();
                config.WithModule<UpdateProductEndpoint>();
                config.WithModule<GetProductsEndpoint>();
                config.WithModule<GetProductByIdEndpoint>();
                config.WithModule<GetProductsByCategoryEndpoint>();

            });
            #endregion

            #region Register Mediator
            services.AddMediatR(config =>
            {
                //Tell Mediator when the commands and handlers is registrated
                config.RegisterServicesFromAssembly(registrationFromAssembly);
                //Execute before find the handler who will receive the command
                config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            });
            #endregion

            #region Register Validators - Fluent Validator
            services.AddValidatorsFromAssembly(registrationFromAssembly);
            #endregion

            #region Register Marter with no cache and no proxy for entities for performance
            services.AddMarten(config =>
            {
                config.Connection(configuration.GetConnectionString("DefaultConnection")!);
                //For Prodution
                // config.AutoCreateSchemaObjects = Weasel.Core.AutoCreate.None;
            }).UseLightweightSessions();
            #endregion

        }
    }
}
