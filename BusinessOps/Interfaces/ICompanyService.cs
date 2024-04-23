using BusinessOps.Data.Entities;
using BusinessOps.Models;
using Services.Repository;

namespace BusinessOps.Interfaces
{
    public interface ICompanyService : IDataRepository<Companies>
    {
        Task<List<CompanyRequestResponse>> GetAllCompanies();
        Task<CompanyRequestResponse> GetCompanyById(int id);
        Task<CompanyRequestResponse> UpsertCompany(CompanyUpsertRequest request);

        Task<bool> Delete(int id);
    }
}
