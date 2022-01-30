using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Domain.ContactBook.Contact;
using TesteBackendEnContact.Core.Interface.ContactBook.Contact;
using TesteBackendEnContact.Repository.Interface;

namespace TesteBackendEnContact.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly ILogger<ContactController> _logger;

        public ContactController(ILogger<ContactController> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// Insere um contato.
        /// </summary>
        /// <response code="200">contato inserido com sucesso</response>
        [HttpPost]
        public async Task<ActionResult<IContact>> Post(Contact contact, [FromServices] IContactRepository contactRepository)
        {
            return Ok(await contactRepository.SaveAsync(contact));
        }

        /// <summary>
        /// Deleta um contato
        /// </summary>
        /// <response code="200">Contato Deletado com sucesso</response>
        [HttpDelete]
        public async Task Delete(int id, [FromServices] IContactRepository contactRepository)
        {
            await contactRepository.DeleteAsync(id);
        }
        /// <summary>
        /// Lista todos os contatos
        /// </summary>
        /// <response code="200">Retorna Contatos</response>
        [HttpGet]
        public async Task<IEnumerable<IContact>> Get([FromServices] IContactRepository contactRepository)
        {
            return await contactRepository.GetAllAsync();
        }
        /// <summary>
        /// Retorna contato a partir do id
        /// </summary>
        /// <response code="200">Retorna um Contatos</response>
        [HttpGet("{id}")]
        public async Task<IContact> Get(int id, [FromServices] IContactRepository contactRepository)
        {
            return await contactRepository.GetAsync(id);
        }
        /// <summary>
        /// Edita contato a partir do id
        /// </summary>
        /// <response code="200">Contato editado com sucesso</response>
        [HttpPut("")]
        public async Task<ActionResult<IContact>> Update(Contact contact, [FromServices] IContactRepository contactRepository)
        {
            return Ok(await contactRepository.UpdateAsync(contact));
        }

    }
}
