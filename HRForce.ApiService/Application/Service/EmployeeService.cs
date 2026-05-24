using HRForce.ApiService.Application.Interfaces;
using HRForce.ApiService.Application.DTO;
using HRForce.ApiService.Domain;

namespace HRForce.ApiService.Application.Service
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<List<EmployeeDto>> GetAllEmployeesAsync()
        {
            var employees = await _employeeRepository.GetAllEmployeesAsync();

            return employees.Select(e => new EmployeeDto
            {
                Id = e.Id,
                EmployeeCode = e.EmployeeCode,
                FullName = e.FullName,
                Email = e.Email,
                PhoneNumber = e.PhoneNumber,
                DepartmentId = e.DepartmentId,
                DepartmentName = e.Department?.DepartmentName,
                Status = e.Status,
                CreatedAt = e.CreatedAt
            }).ToList();
        }

        public async Task<EmployeeDto?> GetEmployeeByIdAsync(int id)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);

            if (employee == null)
                throw new KeyNotFoundException($"Employee with ID {id} not found.");

            return new EmployeeDto
            {
                Id = employee.Id,
                EmployeeCode = employee.EmployeeCode,
                FullName = employee.FullName,
                Email = employee.Email,
                PhoneNumber = employee.PhoneNumber,
                DepartmentId = employee.DepartmentId,
                DepartmentName = employee.Department?.DepartmentName,
                Status = employee.Status,
                CreatedAt = employee.CreatedAt
            };
        }

        public async Task<EmployeeDto> CreateEmployeeAsync(CreateEmployeeDto dto)
        {
            var employee = new Employee
            {
                EmployeeCode = dto.EmployeeCode,
                FullName = dto.FullName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                DepartmentId = dto.DepartmentId,
                Status = dto.Status,
                CreatedAt = DateTime.UtcNow
            };

            var created = await _employeeRepository.CreateAsync(employee);

            return new EmployeeDto
            {
                Id = created.Id,
                EmployeeCode = created.EmployeeCode,
                FullName = created.FullName,
                Email = created.Email,
                PhoneNumber = created.PhoneNumber,
                DepartmentId = created.DepartmentId,
                Status = created.Status,
                CreatedAt = created.CreatedAt
            };
        }

        public async Task<EmployeeDto> UpdateEmployeeAsync(int id, UpdateEmployeeDto dto)
        {
            var existing = await _employeeRepository.GetEmployeeByIdAsync(id);

            if (existing == null)
                throw new KeyNotFoundException($"Employee with ID {id} not found.");

            existing.EmployeeCode = dto.EmployeeCode;
            existing.FullName = dto.FullName;
            existing.Email = dto.Email;
            existing.PhoneNumber = dto.PhoneNumber;
            existing.DepartmentId = dto.DepartmentId;
            existing.Status = dto.Status;

            await _employeeRepository.UpdateAsync(existing);

            return new EmployeeDto
            {
                Id = existing.Id,
                EmployeeCode = existing.EmployeeCode,
                FullName = existing.FullName,
                Email = existing.Email,
                PhoneNumber = existing.PhoneNumber,
                DepartmentId = existing.DepartmentId,
                DepartmentName = existing.Department?.DepartmentName,
                Status = existing.Status,
                CreatedAt = existing.CreatedAt
            };
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            var existing = await _employeeRepository.GetEmployeeByIdAsync(id);

            if (existing == null)
                throw new KeyNotFoundException($"Employee with ID {id} not found.");

            await _employeeRepository.DeleteAsync(id);
        }
    }
}