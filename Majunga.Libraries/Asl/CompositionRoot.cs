using Majunga.Libraries.Infrastructure.IAsl;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Majunga.Libraries.Asl
{
    public static class CompositionRoot
    {
        public static IServiceCollection AddAslServices(this IServiceCollection services, Action<IServiceCollection> configureAslServices)
        {
            // Asl services
            services.AddTransient<ICommandProcessor, CommandProcessor>();
            services.AddTransient<IQueryProcessor, QueryProcessor>();

            configureAslServices(services);

            return services;
        }
    }
}
