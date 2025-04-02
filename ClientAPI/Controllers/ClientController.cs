using Microsoft.AspNetCore.Mvc;
using ClientAPI.Repositories;
using ClientAPI.Models;

namespace ClientAPI.Controllers
{
    [Route("cliente")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ClientRepository _clientRepository;

        public ClientController(ClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        [HttpGet("listar")]
        public ActionResult<IEnumerable<Cliente>> GetClients([FromQuery] string? nome, [FromQuery] string? email, [FromQuery] string? cpf)
        {
            var clients = _clientRepository.GetAllClients();

            if (!string.IsNullOrEmpty(nome))
                clients = clients.Where(c => c.Nome.Contains(nome, StringComparison.OrdinalIgnoreCase)).ToList();

            if (!string.IsNullOrEmpty(email))
                clients = clients.Where(c => c.Email.Equals(email, StringComparison.OrdinalIgnoreCase)).ToList();

            if (!string.IsNullOrEmpty(cpf))
                clients = clients.Where(c => c.CPF == cpf).ToList();

            return Ok(clients);
        }

    }
}
