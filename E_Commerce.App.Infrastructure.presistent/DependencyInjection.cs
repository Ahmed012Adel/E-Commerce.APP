using E_Commerce.App.Infrastructure.presistent._Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace E_Commerce.App.Infrastructure.presistent
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPresistentService(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoreDbContext>((OptionBuilder) =>
            {
                OptionBuilder.UseSqlServer(configuration.GetConnectionString("StoreContext"));
            });

            return services;
        }

    }
}
