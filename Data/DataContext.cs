using employee_management.Models;
using Microsoft.EntityFrameworkCore;

namespace employee_management.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Skill> Skills { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Skill>().HasData(
                new Skill { Id = 1, Name = "Python", Description = "Best programming language." }
            );
        }
    }
}