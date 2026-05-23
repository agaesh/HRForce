using HRForce.ApiService.Domain;

namespace HRForce.ApiService.Application.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<List<Department>> GetAllDepartmentsAsync();

        Task<Department?> GetDepartmentByIdAsync(int id);

        Task<Department> CreateAsync(Department department);

        Task UpdateAsync(Department department);

        Task DeleteAsync(Department department);
    }
}