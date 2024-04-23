using AutoMapper;
using BusinessOps.Interfaces;
using BusinessOps.Models;
using Microsoft.AspNetCore.Mvc;

namespace BusinessOps.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;

        public CompanyController(ICompanyService companyService, IMapper mapper)
        {
            _companyService = companyService;
            _mapper = mapper;
        }

        [HttpGet("GetAllCompanies")]
        public async Task<IActionResult> GetAllCompanies()
        {
            return Ok(await _companyService.GetAllCompanies());
        }

        [HttpGet("GetCompanyById")]
        public async Task<IActionResult> GetCompanyById(int companyId)
        {
            return Ok(await _companyService.GetCompanyById(companyId));
        }

        [HttpPost("UpsertCompany")]
        public async Task<IActionResult> UpsertCompany(CompanyUpsertRequest companyData)
        {
            return Ok(await _companyService.UpsertCompany(companyData));
        }

        [HttpPost("DeleteCompany")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _companyService.Delete(id));
        }
    }
}
