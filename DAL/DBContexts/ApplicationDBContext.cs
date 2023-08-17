using Employee_Portal.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Employee_Portal.DAL.DBContexts
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions options) : base(options){}
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Token> Tokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>();
            modelBuilder.Entity<Token>();
        }

    }
}
