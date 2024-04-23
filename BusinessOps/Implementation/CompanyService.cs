using AutoMapper;
using BusinessOps.Data.Context;
using BusinessOps.Data.Entities;
using BusinessOps.Interfaces;
using BusinessOps.Models;
using Services.Repository;

namespace BusinessOps.Implementation
{
    public class CompanyService : DataRepository<Companies>, ICompanyService
    {
        private readonly BusinessOpsContext _businessOpsContext;
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;

        public CompanyService(BusinessOpsContext businessOpsContext, IMapper mapper, IDepartmentService departmentService) : base(businessOpsContext)
        {
            _businessOpsContext = businessOpsContext;
            _mapper = mapper;
            _departmentService = departmentService;
        }

        public async Task<List<CompanyRequestResponse>> GetAllCompanies()
        {
            List<CompanyRequestResponse> companies = _mapper.Map<List<CompanyRequestResponse>>(await this.FindAsync(x => x.IsDeleted != true));
            List<Departments> departments = _departmentService.GetAll().ToList();

            foreach (CompanyRequestResponse company in companies)
            {
                List<int> companyDepartmentIds = _businessOpsContext.DepartmentCompany.Where(x => x.CompanyId == company.Id).Select(x => x.DepartmentId).ToList();
                company.departments = _mapper.Map<List<DepartmentRequestResponse>>(departments.Where(department => companyDepartmentIds.Contains(department.Id)).ToList());
            }
            return companies;
        }


        public async Task<CompanyRequestResponse> GetCompanyById(int id)
        {
            CompanyRequestResponse company = _mapper.Map<CompanyRequestResponse>(await this.GetAsync(x => x.Id == id));
            if (company != null)
            {
                List<int> companyDepartmentIds = _businessOpsContext.DepartmentCompany.Where(x => x.CompanyId == company.Id).Select(x => x.DepartmentId).ToList();
                List<Departments> departments = _departmentService.GetAll().ToList();
                company.departments = _mapper.Map<List<DepartmentRequestResponse>>(departments.Where(department => companyDepartmentIds.Contains(department.Id)).ToList());
            }

            return company ?? new CompanyRequestResponse();
        }

        public async Task<CompanyRequestResponse> UpsertCompany(CompanyUpsertRequest request)
        {
            Companies company = new();
            List<DepartmentCompany> departmentCompanys = new();
            if (request.Id != 0)
            {
                company = await this.GetAsync(x => x.Id == request.Id);
                if (company != null)
                {
                    company.CompanyName = request.CompanyName;
                    company.UpdatedDate = DateTime.UtcNow;
                    this.Update(company);

                    List<DepartmentCompany> existingDepartment = _businessOpsContext.DepartmentCompany.Where(x => x.CompanyId == request.Id).ToList();
                    _businessOpsContext.DepartmentCompany.RemoveRange(existingDepartment);
                }
            }
            else
            {
                company.CompanyName = request.CompanyName;
                company.CreatedDate = DateTime.UtcNow;
                this.Add(company);
            }
            await this.SaveAsync();

            if (company!= null && company!.Id > 0)
            {
                foreach (int departmentId in request.DepartmentIds)
                {
                    if (departmentId > 0)
                    {
                        DepartmentCompany departmentCompany = new();
                        departmentCompany.CompanyId = company.Id;
                        departmentCompany.DepartmentId = departmentId;
                        departmentCompanys.Add(departmentCompany);
                    }
                }
                _businessOpsContext.DepartmentCompany.AddRange(departmentCompanys);
                await this.SaveAsync();
            }
            return _mapper.Map<CompanyRequestResponse>(company);
        }

        public async Task<bool> Delete(int id)
        {
            if (id != 0)
            {
                Companies company = await this.GetAsync(x => x.Id == id);
                if (company != null)
                {
                    company.IsDeleted = true;
                    company.DeletedDate = DateTime.UtcNow;
                    this.Update(company);
                    await this.SaveAsync();
                    return true;
                }
            }
            return false;
        }
    }
}
