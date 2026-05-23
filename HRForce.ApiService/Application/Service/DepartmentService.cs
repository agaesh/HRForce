using HRForce.ApiService.Application.Interfaces;
using HRForce.ApiService.Infrastructure.Repositories;
using HRForce.ApiService.Domain;
using HRForce.ApiService.Application.DTO;
namespace HRForce.ApiService.Application.Service
{
    public class DepartmentService
    {
        private readonly DepartmentRepository _departmentRepository;
        public DepartmentService(IDepartmentRepository departmentRepository) { 
           _departmentRepository = (DepartmentRepository)departmentRepository;
        }

        public async Task<List<DepartmentDTO>> GetAllDepartmentsAsync()
        {
            var departments = await _departmentRepository.GetAllDepartmentsAsync();

            return departments.Select(d => new DepartmentDTO
        {
                Id = d.Id,
                DepartmentCode = d.DepartmentCode,
                DepartmentName = d.DepartmentName
            }).ToList();
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

        public async Task UpdateDepartmentAsync(UpdateDepartmentDTO uDT)
        {
            var existing = await _departmentRepository.GetDepartmentByIdAsync(uDT.Id);

            if(existing == null)
            {
                throw new Exception($"Department with ID {uDT.Id} not found.");
            }

            existing.DepartmentName = uDT.DepartmentName;
            existing.Status = uDT.Status;
            existing.UpdatedAt = uDT.UpdatedAt;

            await _departmentRepository.UpdateAsync(existing);
        }

        public async Task DeleteDepartmentAsync(int Departmentid)
        {
        
            // Fetching the existing department to ensure it exists before attempting deletion'
            var existing = await _departmentRepository.GetDepartmentByIdAsync(Departmentid);
            
            if (existing == null)
            {
                // Fixed: Use a specific exception for better API error mapping (404)
                throw new KeyNotFoundException($"Department with ID {Departmentid} not found.");
            }
            await _departmentRepository.DeleteAsync(existing);
        }
    }
}
