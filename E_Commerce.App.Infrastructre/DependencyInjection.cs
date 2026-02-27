using E_Commerce.App.Domain.Contract.Infrastructre;
using E_Commerce.App.Infrastructre.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace E_Commerce.App.Infrastructre
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
              services.AddSingleton(typeof(IConnectionMultiplexer), (serviceProvider) =>
              {
                  var connectionString = configuration.GetConnectionString("Redis");
                  var connectionMultiplexerObj = ConnectionMultiplexer.Connect(connectionString!);
                  return connectionMultiplexerObj;
              });

/*            services.AddSingleton<IConnectionMultiplexer>(sp =>
            {
                var options = new ConfigurationOptions
                {
                    EndPoints =
        {
            { "redis-13880.c233.eu-west-1-1.ec2.cloud.redislabs.com", 13880 }
        },

                    User = "default",
                    Password = "qN4gXTOtm1sw9Y9bGLLQVNayuEMqEyUf",
                    AbortOnConnectFail = false,


                };

                return ConnectionMultiplexer.Connect(options);
            });*/

            services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));

            return services;
        }
    }
}