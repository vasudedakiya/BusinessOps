using AutoMapper;
using BusinessOps.Data.Entities;
using BusinessOps.Interfaces;
using BusinessOps.Models;
using Microsoft.AspNetCore.Mvc;

namespace BusinessOps.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;

        public DepartmentController(IDepartmentService departmentService, IMapper mapper)
        {
            _departmentService = departmentService;
            _mapper = mapper;
        }

        [HttpGet("GetAllDepartments")]
        public async Task<IActionResult> GetAllDepartments()
        {
            List<DepartmentRequestResponse> departments = _mapper.Map<List<DepartmentRequestResponse>>(await _departmentService.FindAsync(x => x.IsDeleted != true));
            return Ok(departments);
        }

        [HttpGet("GetDepartmentById")]
        public async Task<IActionResult> GetDepartmentById(int departmentId)
        {
            Departments department = await _departmentService.GetAsync(x => x.Id == departmentId && x.IsDeleted != true);
            DepartmentRequestResponse departmentResponse = new();

            if (department != null)
                departmentResponse = _mapper.Map<DepartmentRequestResponse>(department);

            return Ok(departmentResponse ?? new DepartmentRequestResponse());
        }

        [HttpPost("UpsertDepartment")]
        public async Task<IActionResult> UpsertDepartment(DepartmentRequestResponse departmentData)
        {
            return Ok(await _departmentService.UpsertDepartment(departmentData));
        }

        [HttpPost("DeleteDepartment")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _departmentService.Delete(id));
        }

        [HttpGet("GetDepartmentByCompanyId")]
        public async Task<IActionResult> GetDepartmentByCompanyId(int companyId)
        {
            return Ok(await _departmentService.GetDepartmentsByCompanyId(companyId));
        }
    }
}
