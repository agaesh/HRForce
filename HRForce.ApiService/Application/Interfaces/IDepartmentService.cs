using HRForce.ApiService.Domain;

namespace HRForce.ApiService.Application.Interfaces
{
    public interface IDepartmentService
    {
        Task<List<Department>> GetAllDepartmentsAsync();

        Task<Department?> GetDepartmentByIdAsync(int id);

        Task<Department> CreateDepartmentAsync(Department department);

        Task UpdateDepartmentAsync(Department department);

        Task DeleteDepartmentAsync(Department department);
    }
}