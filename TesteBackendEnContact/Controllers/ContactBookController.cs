using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;
using TesteBackendEnContact.Core.Domain.ContactBook;
using TesteBackendEnContact.Core.Interface.ContactBook;
using TesteBackendEnContact.Repository.Interface;

namespace TesteBackendEnContact.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactBookController : ControllerBase
    {
        private readonly ILogger<ContactBookController> _logger;

        public ContactBookController(ILogger<ContactBookController> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// Insere livro de contatos
        /// </summary>
        /// <response code="200">Livro de contatos inserido com sucesso</response>
        [HttpPost]
        public async Task<IContactBook> Post(ContactBook contactBook, [FromServices] IContactBookRepository contactBookRepository)
        {
            return await contactBookRepository.SaveAsync(contactBook);
        }
        /// <summary>
        /// Deleta livro de contatos
        /// </summary>
        /// <response code="200">Livo de contatos inserido com sucesso</response>
        [HttpDelete]
        public async Task Delete(int id, [FromServices] IContactBookRepository contactBookRepository)
        {
            await contactBookRepository.DeleteAsync(id);
        }
        /// <summary>
        /// Lista todos os livros de contatos
        /// </summary>
        /// <response code="200">Livros de contatos Listados com sucesso</response>
        [HttpGet]
        public async Task<IEnumerable<IContactBook>> Get([FromServices] IContactBookRepository contactBookRepository)
        {
            return await contactBookRepository.GetAllAsync();
        }
        /// <summary>
        /// Lista um livro de contato a partir de id
        /// </summary>
        /// <response code="200">Livro de contato Listado com sucesso</response>
        [HttpGet("{id}")]
        public async Task<IContactBook> Get(int id, [FromServices] IContactBookRepository contactBookRepository)
        {
            return await contactBookRepository.GetAsync(id);
        }
        /// <summary>
        /// Edita um livro de contato a partir de id
        /// </summary>
        /// <response code="200">Livro de contato Editado com sucesso</response>
        [HttpPut("")]
        public async Task<IContactBook> Update(ContactBook contactBook, [FromServices] IContactBookRepository contactBookRepository)
        {
            return await contactBookRepository.UpdateAsync(contactBook);
        }
    }
}
