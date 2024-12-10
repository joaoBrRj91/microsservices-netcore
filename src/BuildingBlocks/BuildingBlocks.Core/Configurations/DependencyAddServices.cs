using BuildingBlocks.Core.Behaviors;
using BuildingBlocks.Core.Exceptions.Handler;
using Carter;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BuildingBlocks.Core.Configurations;

public static class DependencyAddServices
{

    public static void AddCarterService(this IServiceCollection services, Assembly registrationFromAssembly,
        params Type[] modulesTypes)
    {
        services.AddCarter(new DependencyContextAssemblyCatalog(registrationFromAssembly), config =>
        {
            config.WithModules(modulesTypes);
        });
    }

    /// <summary>
    /// Adding in container services pipelines of the Mediator
    /// </summary>
    /// <param name="services">Services Collections for adding Mediator in container</param>
    /// <param name="registrationFromAssembly">Cuurent assembly application for searching  and registers mediators pipelines</param>
    /// <param name="isEnabledPipelineBehavior">Configure if pipeline behaviors (Validaiton and Logging) is enable. Default is true</param>
    public static void AddMediatorWithFluentValidatorServices(this IServiceCollection services, Assembly registrationFromAssembly, bool isEnabledPipelineBehavior = true)
    {
        #region Register Mediator
        services.AddMediatR(config =>
        {
            //Tell Mediator when the commands and handlers is registrated
            config.RegisterServicesFromAssembly(registrationFromAssembly);

            if (isEnabledPipelineBehavior)
            {
                //Execute before find the handler who will receive the command
                config.AddOpenBehavior(typeof(ValidationBehavior<,>));
                config.AddOpenBehavior(typeof(LoggingBehavior<,>));
            }

        });
        #endregion

        #region Register Validators - Fluent Validator
        services.AddValidatorsFromAssembly(registrationFromAssembly);
        #endregion
    }

    public static void AddGlobalExceptionHandler(this IServiceCollection services)
        => services.AddExceptionHandler<CustomExceptionHandler>();
}
