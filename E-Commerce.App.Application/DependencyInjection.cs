using E_Commerce.App.Application.Abstruction.Services;
using E_Commerce.App.Application.Mapping;
using E_Commerce.App.Application.Service;
using Microsoft.Extensions.DependencyInjection;

namespace E_Commerce.App.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicatinServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingProfile));

            services.AddScoped(typeof(IServiceManager) , typeof(ServiceManager));
            return services;
        }

    }
}
