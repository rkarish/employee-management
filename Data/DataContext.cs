using employee_management.Models;
using Microsoft.EntityFrameworkCore;

namespace employee_management.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DbSet<Skill>? Skills { get; set; }

    }
}