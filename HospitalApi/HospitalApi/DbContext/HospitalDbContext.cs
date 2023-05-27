using HospitalApi.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalApi
{
    public class HospitalDbContext : DbContext
    {
        public HospitalDbContext(DbContextOptions<HospitalDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Doctor>().HasData(new Doctor() { Id = 1, Name = "Oleh", Age = 25, Salary = 50 });
            modelBuilder.Entity<Doctor>().HasData(new Doctor() { Id = 2, Name = "Marta", Age = 45, Salary = 60 });
            modelBuilder.Entity<Doctor>().HasData(new Doctor() { Id = 3, Name = "Milly", Age = 36, Salary = 45 });
            modelBuilder.Entity<Doctor>().HasData(new Doctor() { Id = 4, Name = "Mark", Age = 18, Salary = 20 });
        }

        public DbSet<Doctor> Doctors { get; set; }
    }
}
