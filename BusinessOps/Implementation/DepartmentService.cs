using AutoMapper;
using BusinessOps.Data.Context;
using BusinessOps.Data.Entities;
using BusinessOps.Interfaces;
using BusinessOps.Models;
using Microsoft.EntityFrameworkCore;
using Services.Repository;

namespace BusinessOps.Implementation
{
    public class DepartmentService : DataRepository<Departments>, IDepartmentService
    {
        private readonly BusinessOpsContext _businessOpsContext;
        private readonly IMapper _mapper;

        public DepartmentService(BusinessOpsContext businessOpsContext, IMapper mapper) : base(businessOpsContext)
        {
            _businessOpsContext = businessOpsContext;
            _mapper = mapper;
        }

        public async Task<DepartmentRequestResponse> UpsertDepartment(DepartmentRequestResponse request)
        {
            Departments department = new();
            if (request.Id != 0)
            {
                department = await this.GetAsync(x => x.Id == request.Id);
                department.DepartmentName = request.DepartmentName;

                this.Update(department);
                await this.SaveAsync();
            }
            else
            {
                department.DepartmentName = request.DepartmentName;
                this.Add(department);
                await this.SaveAsync();
            }

            return _mapper.Map<DepartmentRequestResponse>(department);
        }

        public async Task<bool> Delete(int id)
        {
            if (id != 0)
            {
                Departments departments = await this.GetAsync(x => x.Id == id);
                departments.IsDeleted = true;

                this.Update(departments);
                await this.SaveAsync();
                return true;
            }
            return false;
        }

        public async Task<List<DepartmentRequestResponse>> GetDepartmentByCompanyId(int companyId)
        {
            List<int> departmentId = await _businessOpsContext.DepartmentCompany.Where(dc=>dc.CompanyId == companyId).Select(x=>x.DepartmentId).ToListAsync();

            List<DepartmentRequestResponse> response = _mapper.Map<List<DepartmentRequestResponse>>(await this.FindAsync(x => departmentId.Contains(x.Id)));

            return response;
        }
    }
}
