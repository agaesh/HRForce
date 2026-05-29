using HRForce.ApiService.Domain;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using HRForce.ApiService.Application.Interfaces;
namespace HRForce.ApiService.Infrastructure.Repositories
{
    public class DepartmentRepository: IDepartmentRepository
    {
        private readonly HrForceDbContext _context;

        public DepartmentRepository(HrForceDbContext context)
        {
            _context = context;
        }

        public IQueryable<Department> GetAllDepartmentsQueryable()
        {
            return _context.Departments.AsQueryable();
        }
        public async Task<Department?> GetDepartmentByIdAsync(int id)
        {
            return await _context.Departments
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> GetDepartmentByCode(string code)
        {
            return await _context.Departments
                .AnyAsync(x => x.DepartmentCode == code);
        }
        public async Task<Department> CreateAsync(Department department)
        {
            try
            {
                await _context.Departments.AddAsync(department);
                await _context.SaveChangesAsync();
                return department;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task UpdateAsync(Department department)
        {
            var existing = await _context.Departments
                .FirstOrDefaultAsync(y => y.Id == department.Id);

            if (existing == null) return;

            // update only allowed fields
            existing.DepartmentName = department.DepartmentName;
            existing.Status = department.Status;
            existing.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(Department department)
        {
            var existingDepartment = await _context.Departments
                .FirstOrDefaultAsync(x => x.Id == department.Id);

            existingDepartment.Status = DepartmentStatus.Deleted;
            existingDepartment.UpdatedAt = DateTime.UtcNow;
;
            await _context.SaveChangesAsync();
        }
    }
}
