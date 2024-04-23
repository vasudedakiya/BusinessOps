using BusinessOps.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessOps.Data.Context
{
    public class BusinessOpsContext : DbContext
    {
        public BusinessOpsContext(DbContextOptions<BusinessOpsContext> options) : base(options)
        {
        }


        public DbSet<Companies> Companies { get; set; }
        public DbSet<Departments> Departments { get; set; }
        public DbSet<DepartmentCompany> DepartmentCompany { get; set; }
        public DbSet<Employees> Employees { get; set; }
    }
}
