using System.ComponentModel.DataAnnotations;
using HRForce.ApiService.Domain;

namespace HRForce.ApiService.Application.DTO
{
    public class UpdateEmployeeDto
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Employee Code is required")]
        [StringLength(20, MinimumLength = 2)]
        public string EmployeeCode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Full Name is required")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public int DepartmentId { get; set; }

        public EmployeeStatus Status { get; set; }
    }
}