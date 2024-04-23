namespace BusinessOps.Models
{
    public class EmployeeRequestResponse
    {
        public int Id { get; set; }

        public string EmployeeName { get; set; }

        public int CompanyId { get; set; }

        public string CompanyName { get; set; }

        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }
    }
}
