using Employee_Portal.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Employee_Portal.DAL.DBContexts
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options){}

        public DbSet<Registration> registrations { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Registration>();
        }

    }
}
