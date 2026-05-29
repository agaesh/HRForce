using HRForce.ApiService.Application.DTO;
using HRForce.ApiService.Domain;
using HRForce.ApiService.Helpers;

namespace HRForce.ApiService.Application.Interfaces
{
    public interface IEmployeeRepository
    {
        IQueryable<Employee> GetAllEmployeesQueryable();
        Task<Employee?> GetEmployeeByIdAsync(int id);
        Task<Employee> CreateAsync(Employee employee);
        Task UpdateAsync(Employee employee);
        Task DeleteAsync(int id);
    }
}
