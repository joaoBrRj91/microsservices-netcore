using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace Basket.API.Configurations
{
    public static class DependencyUseSpecificServices
    {
        public static void UseInfraServices(this WebApplication webApplication)
        {
            //Use Common Services Added In Container -DI
            webApplication.UseCommonApiServices();

            webApplication.UseHealthChecks("/health",
             new HealthCheckOptions
             {
                 ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
             }); ;
        }
    }
}
