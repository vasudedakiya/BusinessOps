using AutoMapper;
using BusinessOps.Data.Entities;
using BusinessOps.Interfaces;
using BusinessOps.Models;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BusinessOps.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        private readonly IDepartmentService _departmentService;
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;

        public EmployeeController(IEmployeeService employeeService, IDepartmentService departmentService, ICompanyService companyService, IMapper mapper)
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
            _companyService = companyService;
            _mapper = mapper;
        }

        /// <summary>
        /// Fatch all employee detail with company & department name.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllEmployees")]
        public async Task<IActionResult> GetAllEmployees()
        {
            List<EmployeeRequestResponse> employees = _mapper.Map<List<EmployeeRequestResponse>>(await _employeeService.FindAsync(x => x.IsDeleted != true));
            List<Companies> companies = await _companyService.FindAsync(x => x.IsDeleted != true);
            List<Departments> departments = await _departmentService.FindAsync(x => x.IsDeleted != true);

            foreach (EmployeeRequestResponse employee in employees)
            {
                employee.CompanyName = companies.FirstOrDefault(c => c.Id == employee.CompanyId)!.CompanyName;
                employee.DepartmentName = departments.FirstOrDefault(d => d.Id == employee.DepartmentId)!.DepartmentName;
            }
            return Ok(employees);
        }

        /// <summary>
        /// Fatch employee detail (name, company, department) by employee id.
        /// </summary>
        /// <param name="employeeId">Employee Id</param>
        /// <returns></returns>
        [HttpGet("GetEmployeeById")]
        public async Task<IActionResult> GetEmployeeById(int employeeId)
        {
            EmployeeRequestResponse employeeResponse = new();
            Employees employee = await _employeeService.GetAsync(x => x.Id == employeeId && x.IsDeleted != true);

            if (employee != null)
            {
                employeeResponse = _mapper.Map<EmployeeRequestResponse>(employee);
                employeeResponse.CompanyName = (await _companyService.GetAsync(c => c.Id == employee.CompanyId))?.CompanyName ?? "";
                employeeResponse.DepartmentName = (await _departmentService.GetAsync(c => c.Id == employee.DepartmentId))?.DepartmentName ?? "";
            }
            return Ok(employeeResponse);
        }

        /// <summary>
        /// Insert or Update Employee Data.
        /// </summary>
        /// <param name="employeeData">Employee Data</param>
        /// <returns></returns>
        [HttpPost("UpsertEmployee")]
        public async Task<IActionResult> UpsertEmployee(EmployeeRequestResponse employeeData)
        {
            return Ok(await _employeeService.UpsertEmployee(employeeData));
        }

        /// <summary>
        /// Delete Employee by employee id.(soft delete)
        /// </summary>
        /// <param name="id">Employee id</param>
        /// <returns></returns>
        [HttpPost("DeleteEmployee")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _employeeService.Delete(id));
        }
    }
}
