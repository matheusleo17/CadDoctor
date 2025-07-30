using CadDoctor.Domain;
using Microsoft.EntityFrameworkCore;

namespace CadDoctor.Infrastructure
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) 
        { 
        }
        public DbSet<AppointmentsModel> appointments { get; set; }
        public DbSet<DoctorModel> doctors { get; set; }
        public DbSet <PatientModel> patients { get; set; }
    }
}
