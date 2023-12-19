using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation
{
    public static class EndpointDefinitionExtensions
    {
        public static void AddEndpointDefinitions(this IServiceCollection services, params Type[] scanMarkers)
        {
            var endpointDefinitions = new List<IEndpointDefinition>();
            foreach (var scanMarker in scanMarkers)
            {
                endpointDefinitions.AddRange(
                    scanMarker.Assembly.ExportedTypes
                        .Where(cls => typeof(IEndpointDefinition).IsAssignableFrom(cls) && !cls.IsInterface && !cls.IsAbstract)
                        .Select(Activator.CreateInstance).Cast<IEndpointDefinition>());
            }
            foreach(var endpointdDefinition in  endpointDefinitions)
            {
                endpointdDefinition.DefineServices(services);
            }

            services.AddSingleton(endpointDefinitions as IReadOnlyCollection<IEndpointDefinition>);
        }
        public static void UseEndpointDefinitions(this WebApplication app)
        {
            var definitions = app.Services.GetRequiredService<IReadOnlyCollection<IEndpointDefinition>>();
            foreach (var definition in definitions)
            {
                definition.DefineEndpoints(app);
            }
        }
    }
}
