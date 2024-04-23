using BusinessOps.Data.Entities;
using BusinessOps.Models;
using Services.Repository;

namespace BusinessOps.Interfaces
{
    public interface IEmployeeService : IDataRepository<Employees>
    {
        Task<EmployeeRequestResponse> UpsertEmployee(EmployeeRequestResponse request);

        Task<bool> Delete(int id);
    }
}
