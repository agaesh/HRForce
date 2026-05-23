using HRForce.ApiService.Domain;

namespace HRForce.ApiService.Application.DTO
{
    public class DepartmentDTO
    {
        public int Id { get; set; }

        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }

        public DepartmentStatus status { get; set; }
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
