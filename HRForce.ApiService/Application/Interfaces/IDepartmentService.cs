using HRForce.ApiService.Domain;
using HRForce.ApiService.Application.DTO;
namespace HRForce.ApiService.Application.Interfaces
{
    public interface IDepartmentService
    {
        Task<List<Department>> GetAllDepartmentsAsync();

        Task<Department?> GetDepartmentByIdAsync(int id);

        Task<Department> CreateDepartmentAsync(CreateDepartmentDto cDT);

        Task UpdateDepartmentAsync(UpdateDepartmentDTO uDT);

        Task DeleteDepartmentAsync(int DepartmentID);
    }
}