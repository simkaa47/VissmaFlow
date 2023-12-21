using Microsoft.Extensions.DependencyInjection;
using VissmaFlow.Core.Contracts.Communication;
using VissmaFlow.Core.Contracts.DataAccess;
using VissmaFlow.Core.Infrastructure.DataAccess;
using VissmaFlow.Core.Services.Communication;
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
            services.AddSingleton<AccessViewModel>();
            services.AddSingleton<MainCommunicationService>();
            services.AddTransient<IComminicationService,ModbusRtuService>();            
            services.AddSingleton<CommunicationVm>();
            services.AddDbContext<VissmaDbContext>();
            services.AddTransient(typeof(IRepository<>), typeof(BaseRepository<>));

            return services;
        }
    }
}
