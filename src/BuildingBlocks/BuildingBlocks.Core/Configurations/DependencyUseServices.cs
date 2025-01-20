using Carter;
using Microsoft.AspNetCore.Builder;

namespace BuildingBlocks.Core.Configurations;

public static class DependencyUseServices
{
    public static void UseCommonApiServices(this WebApplication app,
        bool isExceptionHandlerEnable = false,
        Action<IApplicationBuilder>? configure = default)
    {
        /*Find all class is have implementation ICarterModule (in assembly reigistrated) 
        * and map endpoints defined called AddRouter Method implemented from Interface*/
        app.MapCarter();

        //Add exception handler if is enable
        if (isExceptionHandlerEnable)
        {
            var configureHandler = configure == default ? options => { } : configure;
            app.UseExceptionHandler(configureHandler);
        }
    }
}
