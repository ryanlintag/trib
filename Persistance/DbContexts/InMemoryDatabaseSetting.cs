using Microsoft.EntityFrameworkCore;

namespace Persistance.DbContexts
{
    public static class InMemoryDatabaseSetting
    {
        private static DbContextOptions<AppDbContext> inMemoryDbContextOption = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
                .EnableSensitiveDataLogging(true)
                .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information)
                .Options;

        public static DbContextOptions<AppDbContext> InMemoryDbContextOption { get => inMemoryDbContextOption; }
    }
}
