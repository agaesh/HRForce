using Microsoft.EntityFrameworkCore;

namespace HRForce.ApiService.Infrastructure
{
    public class HrForceDbContext : DbContext
    {
        public HrForceDbContext(DbContextOptions<HrForceDbContext> options)
            : base(options)
        {
        }

        // Add DbSet<T> properties here when you create your entities
        // Example:
        // public DbSet<Department> Departments { get; set; }
    }
}
