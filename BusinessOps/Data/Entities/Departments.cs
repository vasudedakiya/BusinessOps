using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessOps.Data.Entities
{
    public class Departments
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [Column("department_name")]
        public string DepartmentName { get; set; }

        [Column("is_deleted")]
        public Boolean? IsDeleted { get; set; }

        [Column("created_date")]
        public DateTime CreatedDate { get; set; }
        
        [Column("updated_date")]
        public DateTime? UpdatedDate { get; set; }

        [Column("deleted_date")]
        public DateTime? DeletedDate { get; set; }
    }
}
