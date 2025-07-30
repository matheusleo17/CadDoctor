using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CadDoctor.Infrastructure
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDBContext>
    {
        public AppDBContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDBContext>();
            optionsBuilder.UseSqlServer("server=(localdb)\\mssqllocaldb;database=CadSystem;trusted_connection=true;trustServerCertificate=True;");
            return new AppDBContext(optionsBuilder.Options);
        }

    }

}
