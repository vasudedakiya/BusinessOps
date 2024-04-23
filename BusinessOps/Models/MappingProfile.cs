using AutoMapper;
using BusinessOps.Data.Entities;

namespace BusinessOps.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<Companies, CompanyRequestResponse>().ReverseMap();
            this.CreateMap<Departments, DepartmentRequestResponse>().ReverseMap();
            this.CreateMap<EmployeeRequestResponse, Employees>().ReverseMap();
        }
    }
}
