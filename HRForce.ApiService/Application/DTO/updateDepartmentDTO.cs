using HRForce.ApiService.Domain;
using System.ComponentModel.DataAnnotations;

namespace HRForce.ApiService.Application.DTO
{
    public class UpdateDepartmentDTO
    {
        //public int Id { get; set; }

        [Required(ErrorMessage = "Department Name is required")]
        public string DepartmentName { get; set; }

        [Required(ErrorMessage = "Department Name is required")]
        public DepartmentStatus Status { get; set; }
         
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}