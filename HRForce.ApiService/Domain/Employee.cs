namespace HRForce.ApiService.Domain
{
    public class Employee
    {
        public int Id { get; set; }

        public string EmployeeCode { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public int DepartmentId { get; set; }

        public EmployeeStatus Status { get; set; } = EmployeeStatus.Active;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public Department? Department { get; set; }
    }

    public enum EmployeeStatus
    {
        Active = 1,
        Inactive = 2,
        Deleted = 3
    }
}