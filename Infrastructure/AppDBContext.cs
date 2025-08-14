using CadDoctor.Domain;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AppointmentsModel>()
                .HasOne(a => a.Doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DoctorId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<AppointmentsModel>()
                .HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<PatientModel>()
                .HasOne(p => p.Doctor)
                .WithMany() 
                .HasForeignKey(p => p.DoctorId)
                .OnDelete(DeleteBehavior.Restrict); 

            base.OnModelCreating(modelBuilder);
        }
    }
}
