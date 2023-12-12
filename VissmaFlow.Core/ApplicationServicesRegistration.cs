using Microsoft.Extensions.DependencyInjection;
using VissmaFlow.Core.Infrastructure.DataAccess;
using VissmaFlow.Core.ViewModels;

namespace VissmaFlow.Core
{
    public static class ApplicationServicesRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddLogging();
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<ParameterVm>();
            services.AddSingleton<CommunicationVm>();
            services.AddDbContext<VissmaDbContext>();

            return services;
        }
    }
}
