using Carter;
using Microsoft.AspNetCore.Builder;

namespace BuildingBlocks.Core.Configurations;

public static class DependencyUseServices
{
    public static void UseCommonApiServices(this WebApplication app)
    {
        /*Find all class is have implementation ICarterModule (in assembly reigistrated) 
        * and map endpoints defined called AddRouter Method implemented from Interface*/
        app.MapCarter();


        //Global Exception handler defined in BuildingBlocks for all microsservices
        app.UseExceptionHandler(options => { });
    }
}
