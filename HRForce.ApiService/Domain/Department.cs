using System.ComponentModel.DataAnnotations;

namespace HRForce.ApiService.Domain
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(20)]
        public string DepartmentCode { get; set; }

        [MaxLength(100)]
        public string DepartmentName { get; set; }
        public DepartmentStatus Status { get; set; }

        // Optional audit fields (recommended in real systems)
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }
    }

    public enum DepartmentStatus
    {
        Active = 1,
        Inactive = 2,
        Deleted = 3
    }
}