using HRForce.ApiService.Domain;

namespace HRForce.ApiService.Application.DTO
{
    public class EmployeeDto
    {
        public int Id { get; set; }

        public string EmployeeCode { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public int DepartmentId { get; set; }

        public string? DepartmentName { get; set; }

        public EmployeeStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}

