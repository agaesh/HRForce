using System.ComponentModel.DataAnnotations;
namespace HRForce.Web.DTO
{
    public class CreateDepartmentDto
    {
        public required string DepartmentCode { get; set; } // e.g., "HR", "IT", "FIN"
        public required string DepartmentName { get; set; }
        public required string Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
