using Microsoft.EntityFrameworkCore;
using System.IO;

namespace Persistance.DbContexts
{
    public static class SqlLiteDatabaseSetting
    {
        private static string DbPath = System.IO.Path.Join("..\\..\\..\\", "TestDb.db");

        private static DbContextOptions<AppDbContext> sqlLiteDbContextOption = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite($"Data Source={DbPath}")
                //.EnableSensitiveDataLogging(true)
                //.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information)
                .Options;

        public static DbContextOptions<AppDbContext> SqlLiteDbContextOption { get { return sqlLiteDbContextOption; } }
    }
}
