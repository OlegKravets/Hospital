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

        public DbSet<User> Users { get; set; }
    }
}
