using HRForce.ApiService.Domain;
using HRForce.ApiService.Application.DTO;
using Microsoft.AspNetCore.Mvc;
namespace HRForce.ApiService.Application.Interfaces
{
    public interface IDepartmentService
    {
        Task<List<DepartmentDTO>> GetAllDepartmentsAsync();

        Task<DepartmentDTO?> GetDepartmentByIdAsync(int id);

        Task<DepartmentDTO> CreateDepartmentAsync(CreateDepartmentDto cDTO);

        Task<DepartmentDTO> UpdateDepartmentAsync(int id,UpdateDepartmentDTO uDT);

        Task DeleteDepartmentAsync(int DepartmentID);
    }
}