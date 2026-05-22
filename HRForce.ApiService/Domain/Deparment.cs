using System.ComponentModel.DataAnnotations;

namespace HRForce.ApiService.Domain
{
    public class Department
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Department Code is required")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Department Code must be between 2 and 20 characters")]
        public string DepartmentCode { get; set; }

        [Required(ErrorMessage = "Department Name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Department Name must be between 2 and 100 characters")]
        public string DepartmentName { get; set; }

        [Required(ErrorMessage = "Status is required")]
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