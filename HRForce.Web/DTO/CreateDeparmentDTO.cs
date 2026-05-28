using System.ComponentModel.DataAnnotations;
namespace HRForce.Web.DTO
{
    public class CreateDepartmentDto
    {
        public string DepartmentCode { get; set; } // e.g., "HR", "IT", "FIN"
        public string DepartmentName { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
