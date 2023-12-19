using Application.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistance.DbContexts;
using Persistance.Repositories;

namespace Persistance
{
    public static class DependencyInjection
    {
        private static string DbPath = System.IO.Path.Join("..\\", "TestDb.db");
        public static IServiceCollection AddPersistance(this IServiceCollection services)
        {
            var assembly = typeof(DependencyInjection).Assembly;
            services.AddDbContext<AppDbContext>(option => option.UseSqlite($"Data Source={DbPath}"));
            services.AddTransient<IUserRepository, UserRepository>();
            return services;
        }
    }
}
