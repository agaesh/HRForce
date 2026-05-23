using HRForce.ApiService.Application.Interfaces;
using HRForce.ApiService.Infrastructure.Repositories;
namespace HRForce.ApiService.Application.Service
{
    public class DepartmentService
    {
        public readonly DepartmentRepository _departmentRepository;
        public DepartmentService(IDepartmentRepository departmentRepository) { 
           _departmentRepository = (DepartmentRepository)departmentRepository;
        }
    }
}
