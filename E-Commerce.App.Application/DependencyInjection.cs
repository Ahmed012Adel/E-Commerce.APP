using E_Commerce.App.Application.Abstruction.Services;
using E_Commerce.App.Application.Mapping;
using E_Commerce.App.Application.Service;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.App.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicatinServices(this IServiceCollection services)
        {
            services.AddAutoMapper( M => M.AddProfile(new MappingProfile()));

            services.AddScoped(typeof(IServiceManager) , typeof(ServiceManager));
            return services;
        }

    }
}
