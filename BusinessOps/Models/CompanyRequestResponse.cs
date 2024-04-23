namespace BusinessOps.Models
{
    public class CompanyRequestResponse
    {
        public int Id { get; set; }

        public string CompanyName { get; set; }

        public List<DepartmentRequestResponse>? departments { get; set; }
    }
}
