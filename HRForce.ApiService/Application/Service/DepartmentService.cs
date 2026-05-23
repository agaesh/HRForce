using HRForce.ApiService.Application.Interfaces;
using HRForce.ApiService.Infrastructure.Repositories;
using HRForce.ApiService.Domain;
using HRForce.ApiService.Application.DTO; 
namespace HRForce.ApiService.Application.Service
{
    public class DepartmentService
    {
        public readonly DepartmentRepository _departmentRepository;
        public DepartmentService(IDepartmentRepository departmentRepository) { 
           _departmentRepository = (DepartmentRepository)departmentRepository;
        }

        public Task<List<Department>> GetAllDepartmentsAsync()
        {
            return _departmentRepository.GetAllDepartmentsAsync();
        }

        public Task<Department?> GetDepartmentByIdAsync(int id)
        {
            return _departmentRepository.GetDepartmentByIdAsync(id);
        }

        public Task<Department> CreateDepartmentAsync(CreateDepartmentDto cDTO)
        {
            // Creating DTO and mapping it to domain entity helps prevent direct exposure of database entities through the API contract
            var department = new Department
            {
                DepartmentCode = cDTO.DepartmentCode,
                DepartmentName = cDTO.DepartmentName,
                Status = cDTO.Status,
                CreatedAt = cDTO.CreatedAt
            };

            return _departmentRepository.CreateAsync(department);
        }

        public Task UpdateDepartmentAsync(Department department)
        {
            return _departmentRepository.UpdateAsync(department);
        }

        public Task DeleteDepartmentAsync(Department department)
        {
            return _departmentRepository.DeleteAsync(department);
        }
    }
}
