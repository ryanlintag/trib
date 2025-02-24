using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ApplicationInitialization
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediator();
            return services;
        }
    }
}
