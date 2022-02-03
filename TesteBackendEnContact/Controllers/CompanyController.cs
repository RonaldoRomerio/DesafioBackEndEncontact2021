using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using TesteBackendEnContact.Controllers.Models;
using TesteBackendEnContact.Core.Interface.ContactBook.Company;
using TesteBackendEnContact.Repository.Interface;

namespace TesteBackendEnContact.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly ILogger<CompanyController> _logger;

        public CompanyController(ILogger<CompanyController> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// Insere uma empresa.
        /// </summary>
        /// <response code="200">Empresa inserida com sucesso</response>
        [HttpPost]
        public async Task<ActionResult<ICompany>> Post(CompanyRequest company, [FromServices] ICompanyRepository companyRepository)
        {
            return Ok(await companyRepository.SaveAsync(company.ToCompany()));
        }

        /// <summary>
        /// Deleta uma Empresa
        /// </summary>
        /// <response code="200">Empresa Deletada com sucesso</response>
        [HttpDelete]
        public async Task Delete(int id, [FromServices] ICompanyRepository companyRepository)
        {
            await companyRepository.DeleteAsync(id);
        }
        /// <summary>
        /// Lista todas as empresa
        /// </summary>
        /// <response code="200">Retorna empresas</response>
        [HttpGet("{skip}/{take}")]
        public async Task<IEnumerable<ICompany>> Get([FromServices] ICompanyRepository companyRepository,
                                                    int skip = 1, int take = 20)
        {
            return await companyRepository.GetAllAsync(skip, take);
        }
        /// <summary>
        /// Retorna empresa a partir do id
        /// </summary>
        /// <response code="200">Retorna empresa</response>
        [HttpGet("{id}")]
        public async Task<ICompany> Get(int id, [FromServices] ICompanyRepository companyRepository)
        {
            return await companyRepository.GetAsync(id);
        }
        /// <summary>
        /// Edita empresa a partir do id
        /// </summary>
        /// <response code="200">Empresa editada com sucesso</response>
        [HttpPut("")]
        public async Task<ActionResult<ICompany>> Update(CompanyRequest company, [FromServices] ICompanyRepository companyRepository)
        {
            return Ok(await companyRepository.UpdateAsync(company.ToCompany()));
        }
    }
}
