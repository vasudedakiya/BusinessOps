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

        /// <summary>
        /// Fatch All Compaines Data with department detail.
        /// </summary>
        /// <returns></returns>

        [HttpGet("GetAllCompanies")]
        public async Task<IActionResult> GetAllCompanies()
        {
            return Ok(await _companyService.GetAllCompanies());
        }

        /// <summary>
        /// Fatch Company Data based on companyId
        /// </summary>
        /// <param name="companyId">CompanyId</param>
        /// <returns></returns>
        [HttpGet("GetCompanyById")]
        public async Task<IActionResult> GetCompanyById(int companyId)
        {
            return Ok(await _companyService.GetCompanyById(companyId));
        }

        /// <summary>
        /// Insert Or update company data with departmentIds also.
        /// </summary>
        /// <param name="companyData">Company Data Object.</param>
        /// <returns></returns>
        [HttpPost("UpsertCompany")]
        public async Task<IActionResult> UpsertCompany(CompanyUpsertRequest companyData)
        {
            return Ok(await _companyService.UpsertCompany(companyData));
        }


        /// <summary>
        /// Delete Company data.(Soft Delete)
        /// </summary>
        /// <param name="id">Company Id</param>
        /// <returns></returns>
        [HttpPost("DeleteCompany")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _companyService.Delete(id));
        }
    }
}
