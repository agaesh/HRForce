using HRForce.ApiService.Application.DTO;
using HRForce.ApiService.Helpers;

namespace HRForce.ApiService.Application.Interfaces
{
    public interface IEmployeeService
    {
        Task<PagedResult<EmployeeDto>> GetAllEmployeesQueryable(
            int pageNumber = 1, int pageSize = 10, string search = null,string status = null);
        Task<EmployeeDto?> GetEmployeeByIdAsync(int id);

        Task<EmployeeDto> CreateEmployeeAsync(CreateEmployeeDto dto);

        Task<EmployeeDto> UpdateEmployeeAsync(int id, UpdateEmployeeDto dto);

        Task DeleteEmployeeAsync(int id);
    }
}
