using HRForce.ApiService.Application.DTO;
using HRForce.ApiService.Application.Interfaces;
using HRForce.ApiService.Domain;
using HRForce.ApiService.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using HRForce.ApiService.Helpers;
namespace HRForce.ApiService.Application.Service
{
    public class DepartmentService: IDepartmentService
    {
        // FIX 1: Change the field type to the interface
        private readonly IDepartmentRepository _departmentRepository;

        // FIX 2: Remove the dangerous explicit cast
        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }
        public async Task<PagedResult<DepartmentDTO>> GetAllDepartmentsQueryable(int pageNumber = 1, int pageSize = 10, string search = null)
        {
            var query = _departmentRepository.GetAllDepartmentsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(d =>
                    d.DepartmentName.Contains(search) ||
                    d.DepartmentCode.Contains(search));
            }

            var totalCount = await query.CountAsync();

         
            var departments = await query
                .OrderBy(d => d.DepartmentName)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var items = departments.Select(d => new DepartmentDTO
            {
                Id = d.Id,
                DepartmentCode = d.DepartmentCode,
                DepartmentName = d.DepartmentName,
                Status = d.Status,
                CreatedAt = d.CreatedAt,
                UpdatedAt = d.UpdatedAt
            }).ToList();

            return new PagedResult<DepartmentDTO>
            {
                Items = items,            //filtered and paginated list of items
                TotalCount = totalCount, // total database count for pagination
                PageNumber = pageNumber, // add Page number like Page 1, page 2
                PageSize = pageSize, // it describes how much in a page
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };
        }

        public async Task<DepartmentDTO?> GetDepartmentByIdAsync(int id)
        {
            var department = await _departmentRepository.GetDepartmentByIdAsync(id);

            if (department == null)
                throw new KeyNotFoundException($"Department with ID {id} not found.");

            return new DepartmentDTO
            {
                Id = department.Id,
                DepartmentCode = department.DepartmentCode,
                DepartmentName = department.DepartmentName,
                Status = department.Status,
                CreatedAt = department.CreatedAt,
                UpdatedAt = department.UpdatedAt
            }; 
        }

        public async Task<DepartmentDTO> CreateDepartmentAsync(CreateDepartmentDto cDTO)
        {
            try
            {
                var exists = await _departmentRepository.GetDepartmentByCode(cDTO.DepartmentCode);

                if (exists)
                    throw new Exception("Department code already exists");

                var department = new Department
                {
                    DepartmentCode = cDTO.DepartmentCode,
                    DepartmentName = cDTO.DepartmentName,
                    Status = cDTO.Status,
                    CreatedAt = cDTO.CreatedAt
                };

                var created = await _departmentRepository.CreateAsync(department);

                return new DepartmentDTO
                {
                    Id = created.Id,
                    DepartmentCode = created.DepartmentCode,
                    DepartmentName = created.DepartmentName,
                    Status = (DepartmentStatus)created.Status,
                    CreatedAt = created.CreatedAt
                };
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task <DepartmentDTO> UpdateDepartmentAsync(int id, UpdateDepartmentDTO uDT)
        {
            var existing = await _departmentRepository
                .GetDepartmentByIdAsync(id);

            if (existing == null)
            {
                throw new KeyNotFoundException(
                    $"Department with ID {id} not found.");
            }

            existing.DepartmentName = uDT.DepartmentName;
            existing.Status = uDT.Status;
            existing.UpdatedAt = uDT.UpdatedAt;

            await _departmentRepository.UpdateAsync(existing);

            return new DepartmentDTO
            {
                Id = existing.Id,
                DepartmentCode = existing.DepartmentCode,
                DepartmentName = existing.DepartmentName,
                Status = existing.Status,
                CreatedAt = existing.CreatedAt,
                UpdatedAt = existing.UpdatedAt
            };
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
