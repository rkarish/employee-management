using employee_management.Models;
using Microsoft.EntityFrameworkCore;

namespace employee_management.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Skill>? Skills { get; set; }
        public DbSet<Employee>? Employees { get; set; }
        public DbSet<EmployeeSkill>? EmployeeSkills { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeSkill>().HasKey(es => new { es.EmployeeId, es.SkillId });
            modelBuilder.Entity<Skill>().HasData(
                new Skill { Id = 1, Name = "Python", Description = "Best programming language.", DateCreated = DateTime.Now }
            );
            modelBuilder.Entity<Employee>().HasData(
                new Employee { Id = 1, FirstName = "Robert", LastName = "Karish", Details = "Software Engineer", DateCreated = DateTime.Now }
            );
            modelBuilder.Entity<EmployeeSkill>().HasData(
                new EmployeeSkill { EmployeeId = 1, SkillId = 1 }
            );
        }
    }
}