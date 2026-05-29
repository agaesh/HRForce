using HRForce.ApiService.Domain;
using OpenTelemetry.Trace;

namespace HRForce.ApiService.Application.Interfaces
{
    public interface IDepartmentRepository
    {
        IQueryable<Department> GetAllDepartmentsQueryable();

        Task<Department?> GetDepartmentByIdAsync(int id);
        Task<bool> GetDepartmentByCode(string code);

        Task<Department> CreateAsync(Department department);

        Task UpdateAsync(Department department);

        Task DeleteAsync(Department department);

        Task<IEnumerable<Department>> GetDepartmentsByStatusAsync(string status);
    }
}
