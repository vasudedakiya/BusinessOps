using AutoMapper;
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
            DepartmentRequestResponse department = _mapper.Map<DepartmentRequestResponse>(await _departmentService.GetAsync(x => x.Id == departmentId));
            return Ok(department);
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
            return Ok(await _departmentService.GetDepartmentByCompanyId(companyId));
        }
    }
}
