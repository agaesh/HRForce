using HRForce.ApiService.Domain;
using HRForce.ApiService.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HRForce.ApiService.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly HrForceDbContext _context;

        public EmployeeRepository(HrForceDbContext context)
        {
            _context = context;
        }

        public IQueryable<Employee> GetAllEmployeesQueryable()
        {
            return _context.Employees
                .Include(e => e.Department)
                .AsQueryable();
        }

        public async Task<Employee?> GetEmployeeByIdAsync(int id)
        {
            return await _context.Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Employee> CreateAsync(Employee employee)
        {
            await _context.Employees.AddAsync(employee);
            await _context.SaveChangesAsync();
            return employee;
        }

        public async Task UpdateAsync(Employee employee)
        {
            var existing = await _context.Employees
                .FirstOrDefaultAsync(x => x.Id == employee.Id);

            if (existing == null) return;

            existing.FullName = employee.FullName;
            existing.Email = employee.Email;
            existing.PhoneNumber = employee.PhoneNumber;
            existing.DepartmentId = employee.DepartmentId;
            existing.Status = employee.Status;
            existing.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var existing = await _context.Employees
                .FirstOrDefaultAsync(x => x.Id == id);

            if (existing == null) return;

            existing.Status = EmployeeStatus.Deleted;
            existing.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }
    }
}