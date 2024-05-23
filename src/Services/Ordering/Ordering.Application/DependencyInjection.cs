using Microsoft.Extensions.DependencyInjection;
using BuildingBlocks.Messaging.MassTransit;
using Microsoft.Extensions.Configuration;
using BuildingBlocks.Behaviors;
using System.Reflection;
using Microsoft.FeatureManagement;

namespace Ordering.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
                config.AddOpenBehavior(typeof(ValidationBehavior<,>));
                config.AddOpenBehavior(typeof(LoggingBehavior<,>));
            });

            services.AddFeatureManagement();

            services.AddMessageBroker(configuration, Assembly.GetExecutingAssembly());

            return services;
        }
    }
}