using Microsoft.EntityFrameworkCore;
using HRForce.ApiService.Domain;
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
        public DbSet<Department> Departments { get; set; }
        public DbSet<Employee> Employees { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>()
                .Property(d => d.Status)
                .HasConversion<string>();
        
            modelBuilder.Entity<Department>()
                .HasIndex(d => d.DepartmentCode)
                .IsUnique();

            modelBuilder.Entity<Employee>()
                .Property(e => e.Status)
                .HasConversion<string>();


            modelBuilder.Entity<Employee>()
                .HasIndex(e => e.EmployeeCode)
                .IsUnique();

            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Department)
                .WithMany()
                .HasForeignKey(e => e.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
