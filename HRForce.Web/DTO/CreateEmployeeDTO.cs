using System.ComponentModel.DataAnnotations;

namespace HRForce.Web.DTO
{
    public class CreateEmployeeDTO
    {
    
        public string EmployeeCode { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;
        public int DepartmentId { get; set; }

        public string Status { get; set; }
    }
}
