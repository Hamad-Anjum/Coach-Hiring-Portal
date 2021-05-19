using System.IO;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CHS.Domains.Context
{
    public class DbContextFactory : IDesignTimeDbContextFactory<CHSDbContext>
    {
        public CHSDbContext CreateDbContext(string[] args)
        {
            //var builder = new DbContextOptionsBuilder<AppDbContext>();
            //builder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=TestDb;Trusted_Connection=True;MultipleActiveResultSets=true",
            //    optionsBuilder => optionsBuilder.MigrationsAssembly(typeof(AppDbContext).GetTypeInfo().Assembly.GetName().Name));

            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(@"D:\Projects\CoachHiringSystem\CHS\CHS\CHS.Api\appsettings.json").Build();
            var builder = new DbContextOptionsBuilder<CHSDbContext>();
            var connectionString = configuration.GetConnectionString("DatabaseConnection");
            builder.UseSqlServer(connectionString);

            return new CHSDbContext(builder.Options);
        }
    }
}
