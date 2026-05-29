namespace HRForce.Web.DTO
{
    public class DepartmentDTO
    {
        public int Id { get; set; }

        public string DepartmentCode { get; set; }
        public string DepartmentName { get; set; }

        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
    }
}
