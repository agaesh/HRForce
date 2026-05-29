using System.ComponentModel.DataAnnotations;

namespace HRForce.Web.DTO
{
    public class UpdateDepartmentDTO
    {
        public string DepartmentName { get; set; }

        public string Status { get; set; }

        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
