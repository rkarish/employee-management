using employee_management.Models;
using Microsoft.EntityFrameworkCore;

namespace employee_management.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Skill>? Skills { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
    }
}