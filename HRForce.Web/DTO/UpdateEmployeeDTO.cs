using System.ComponentModel.DataAnnotations;

namespace HRForce.Web.DTO
{
    public class UpdateEmployeeDTO
    {
        public int Id { get; set; }
        public string EmployeeCode { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        [Required]
        public int DepartmentId { get; set; }

        public string Status { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
