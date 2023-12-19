using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Presentation.Authorization;
using Serilog;

namespace Presentation
{
    public static class DependencyInjection
    {
        public static WebApplicationBuilder BuildPresentationHost(this WebApplicationBuilder builder)
        {
            ArgumentNullException.ThrowIfNull(builder);
            builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));
            return builder;
        }
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddEndpointDefinitions(typeof(DependencyInjection));
            return services;
        }

        public static WebApplication UsePresentation(this WebApplication app)
        {
            //Always request x-api-key headers for all internal api queries
            app.UseMiddleware<ApiKeyMiddleware>();
            app.UseEndpointDefinitions();
            return app;
        }
    }
}
