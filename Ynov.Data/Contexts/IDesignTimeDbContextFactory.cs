using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Ynov.Data.Contexts
{
    public class BoardDbContextFactory : IDesignTimeDbContextFactory<TrellodDbContext>
    {
        public TrellodDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var builder = new DbContextOptionsBuilder<TrellodDbContext>();
            
            DotNetEnv.Env.Load();
            var connectionString = System.Environment.GetEnvironmentVariable("YNOV_BOARD_POSTGRES"); 
            builder.UseNpgsql(connectionString);

            return new TrellodDbContext(builder.Options);
        }
    }
}