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
        private readonly IMapper _mapper;

        public EmployeeService(BusinessOpsContext businessOpsContext, IMapper mapper) : base(businessOpsContext)
        {
            _businessOpsContext = businessOpsContext;
            _mapper = mapper;
        }

        public async Task<EmployeeRequestResponse> UpsertEmployee(EmployeeRequestResponse request)
        {
            Employees employee = new();
            if (request.Id != 0)
            {
                employee = await this.GetAsync(x => x.Id == request.Id);
                employee = _mapper.Map<Employees>(request);
                this.Update(employee);
            }
            else
            {
                employee = _mapper.Map<Employees>(request);
                this.Add(employee);
            }
            await this.SaveAsync();

            return _mapper.Map<EmployeeRequestResponse>(employee);
        }

        public async Task<bool> Delete(int id)
        {
            if (id != 0)
            {
                Employees employees = await this.GetAsync(x => x.Id == id);
                employees.IsDeleted = true;

                this.Update(employees);
                await this.SaveAsync();
                return true;
            }
            return false;
        }
    }
}
