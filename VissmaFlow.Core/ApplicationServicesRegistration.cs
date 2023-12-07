using Microsoft.Extensions.DependencyInjection;

namespace VissmaFlow.Core
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddLogging();
            return services;
        }
    }
}
