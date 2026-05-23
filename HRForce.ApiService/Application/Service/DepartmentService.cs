using HRForce.ApiService.Application.Interfaces;
using HRForce.ApiService.Infrastructure.Repositories;
using HRForce.ApiService.Domain;
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

        public Task<Department> CreateDepartmentAsync(Department department)
        {
            return _departmentRepository.CreateAsync(department);
        }
    }
}
