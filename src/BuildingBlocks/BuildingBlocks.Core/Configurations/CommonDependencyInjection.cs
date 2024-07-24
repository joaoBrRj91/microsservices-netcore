using BuildingBlocks.Core.Behaviors;
using BuildingBlocks.Core.Exceptions.Handler;
using Carter;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BuildingBlocks.Core.Configurations;

public static class CommonDependencyInjection
{

    public static void AddCarterService(this IServiceCollection services, Assembly registrationFromAssembly, 
        params Type[] modulesTypes)
    {
        services.AddCarter(new DependencyContextAssemblyCatalog(registrationFromAssembly), config =>
        {
            config.WithModules(modulesTypes);
        });
    }

    public static void AddMediatorWithFluentValidatorServices(this IServiceCollection services, Assembly registrationFromAssembly)
    {
        #region Register Mediator
        services.AddMediatR(config =>
        {
            //Tell Mediator when the commands and handlers is registrated
            config.RegisterServicesFromAssembly(registrationFromAssembly);
            //Execute before find the handler who will receive the command
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
            config.AddOpenBehavior(typeof(LoggingBehavior<,>));

        });
        #endregion

        #region Register Validators - Fluent Validator
        services.AddValidatorsFromAssembly(registrationFromAssembly);
        #endregion
    }

    public static void AddGlobalExceptionHandler(this IServiceCollection services) 
        => services.AddExceptionHandler<CustomExceptionHandler>();
}
