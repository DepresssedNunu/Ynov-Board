// using Microsoft.EntityFrameworkCore;
// using Microsoft.EntityFrameworkCore.Design;
// using Microsoft.Extensions.Configuration;
//
//
// namespace Ynov.Data.Contexts
// {
//     public class BoardDbContextFactory : IDesignTimeDbContextFactory<BoardDbContext>
//     {
//         public BoardDbContext CreateDbContext(string[] args)
//         {
//             IConfigurationRoot configuration = new Configuration()
//                 .SetBasePath(Directory.GetCurrentDirectory())
//                 .AddJsonFile("./Ynov/Ynov.API/appsettings.json")
//                 .Build();
//
//             var builder = new DbContextOptionsBuilder<BoardDbContext>();
//             var connectionString = configuration.GetConnectionString("DefaultConnection");
//             builder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
//
//             return new BoardDbContext(builder.Options);
//         }
//     }
// }