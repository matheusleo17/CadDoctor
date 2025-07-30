using Microsoft.EntityFrameworkCore;

namespace CadDoctor.Infrastructure
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) 
        { 
        }
    }
}
