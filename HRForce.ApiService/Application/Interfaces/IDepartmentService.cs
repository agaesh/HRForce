using HRForce.ApiService.Application.DTO;
using HRForce.ApiService.Domain;
using HRForce.ApiService.Helpers;
using Microsoft.AspNetCore.Mvc;
namespace HRForce.ApiService.Application.Interfaces
{
    public interface IDepartmentService
    {
        Task<PagedResult<DepartmentDTO>> GetAllDepartmentsQueryable(int pageNumber = 1, int pageSize = 10, string search = null);


        Task<DepartmentDTO?> GetDepartmentByIdAsync(int id);

        Task<IEnumerable<DepartmentLookUPDTO>> GetDepartmentsByStatusAsync(string Status);

        Task<DepartmentDTO> CreateDepartmentAsync(CreateDepartmentDto cDTO);

        Task<DepartmentDTO> UpdateDepartmentAsync(int id,UpdateDepartmentDTO uDT);

        Task DeleteDepartmentAsync(int DepartmentID);
    }
}