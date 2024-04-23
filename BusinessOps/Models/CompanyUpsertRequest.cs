namespace BusinessOps.Models
{
    public class CompanyUpsertRequest
    {
        public int Id { get; set; }

        public string CompanyName { get; set; }

        public List<int> DepartmentIds { get; set; }
    }
}
