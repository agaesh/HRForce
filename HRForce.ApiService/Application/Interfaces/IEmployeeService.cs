using HRForce.ApiService.Application.DTO;

namespace HRForce.ApiService.Application.Interfaces
{
    public interface IEmployeeService
    {
        Task<List<EmployeeDto>> GetAllEmployeesAsync();
        Task<EmployeeDto?> GetEmployeeByIdAsync(int id);

        Task<EmployeeDto> CreateEmployeeAsync(CreateEmployeeDto dto);

        Task<EmployeeDto> UpdateEmployeeAsync(int id, UpdateEmployeeDto dto);

        Task DeleteEmployeeAsync(int id);
    }
}
