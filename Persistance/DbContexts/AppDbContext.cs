using Domain.Users;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Persistance.DbContexts
{
    public sealed class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            //Register all complex types whereever needed
            configurationBuilder.RegisterUserComplexTypeProperties();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Look for per entity configuration found in EntityConfigurations folder
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserDomainEvent> UserEvents { get; set; }
    }
}
