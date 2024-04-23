using AutoMapper;
using BusinessOps.Data.Context;
using BusinessOps.Data.Entities;
using BusinessOps.Interfaces;
using BusinessOps.Models;
using Services.Repository;

namespace BusinessOps.Implementation
{
    public class EmployeeService : DataRepository<Employees>, IEmployeeService
    {
        private readonly BusinessOpsContext _businessOpsContext;
        private readonly ICompanyService _companyService;
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;

        public EmployeeService(BusinessOpsContext businessOpsContext, ICompanyService companyService, IDepartmentService departmentService, IMapper mapper) : base(businessOpsContext)
        {
            _businessOpsContext = businessOpsContext;
            _companyService = companyService;
            _departmentService = departmentService;
            _mapper = mapper;
        }

        public async Task<EmployeeRequestResponse> UpsertEmployee(EmployeeRequestResponse request)
        {
            Employees employee = new();
            Companies company = await _companyService.GetAsync(x => x.Id == request.CompanyId && x.IsDeleted != true);
            Departments department = await _departmentService.GetAsync(x => x.Id == request.DepartmentId && x.IsDeleted != true);

            if (company != null && department != null)
            {
                if (request.Id != 0)
                {
                    employee = await this.GetAsync(x => x.Id == request.Id);
                    if (employee != null)
                    {
                        employee = _mapper.Map<Employees>(request);
                        this.Update(employee);
                    }
                }
                else
                {
                    employee = _mapper.Map<Employees>(request);
                    this.Add(employee);
                }
                await this.SaveAsync();
            }
            return employee != null ? _mapper.Map<EmployeeRequestResponse>(employee) : new EmployeeRequestResponse();
        }

        public async Task<bool> Delete(int id)
        {
            if (id != 0)
            {
                Employees employees = await this.GetAsync(x => x.Id == id);
                if (employees != null)
                {
                    employees.IsDeleted = true;
                    this.Update(employees);
                    await this.SaveAsync();
                    return true;
                }
            }
            return false;
        }
    }
}
