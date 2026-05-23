using HRForce.ApiService.Domain;
using System.ComponentModel.DataAnnotations;

namespace HRForce.ApiService.Application.DTO
{
    public class CreateDepartmentDto
    {
        [Required(ErrorMessage = "Department Code is required")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Department Code must be between 2 and 20 characters")]
        public required string DepartmentCode { get; set; } // e.g., "HR", "IT", "FIN"

        [Required(ErrorMessage = "Department Name is required")]
        public required string DepartmentName { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public required DepartmentStatus Status { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}