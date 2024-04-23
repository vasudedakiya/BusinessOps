using BusinessOps.Data.Entities;
using BusinessOps.Models;
using Services.Repository;

namespace BusinessOps.Interfaces
{
    public interface IDepartmentService : IDataRepository<Departments>
    {
        Task<DepartmentRequestResponse> UpsertDepartment(DepartmentRequestResponse request);

        Task<bool> Delete(int id);

        Task<List<DepartmentRequestResponse>> GetDepartmentByCompanyId(int companyId);
    }
}
