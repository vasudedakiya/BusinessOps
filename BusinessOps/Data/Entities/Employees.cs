using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessOps.Data.Entities
{
    public class Employees
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        public string EmployeeName { get; set; }

        [Required]
        [Column("company_id")]
        [ForeignKey("Company")]
        public int CompanyId { get; set; }
        public Companies Company { get; set; }

        [Required]
        [Column("department_id")]
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public Departments Department { get; set; }

        [Column("is_deleted")]
        public Boolean? IsDeleted { get; set; }
    }
}
