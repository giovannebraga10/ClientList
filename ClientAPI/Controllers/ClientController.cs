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

        [HttpPost("criar")]
        public ActionResult<Cliente> CreateClient([FromBody] Cliente cliente)
        {
            if (cliente == null)
            {
                return BadRequest("Dados inválidos.");
            }

            _clientRepository.AddClient(cliente);

            return CreatedAtAction(nameof(GetClients), new { id = cliente.Id }, cliente);
        }

        [HttpPut("atualizar/{id}")]
        public ActionResult UpdateClient(int id, [FromBody] Cliente cliente)
        {
            if (cliente == null || id <= 0)
            {
                return BadRequest("Dados inválidos.");
            }

            var existingClient = _clientRepository.GetClientById(id);
            if (existingClient == null)
            {
                return NotFound("Cliente não encontrado.");
            }

            existingClient.Nome = cliente.Nome;
            existingClient.Email = cliente.Email;
            existingClient.CPF = cliente.CPF;
            existingClient.RG = cliente.RG;

            _clientRepository.UpdateClient(existingClient);

            return NoContent();
        }

        [HttpDelete("deletar/{id}")]
        public ActionResult DeleteClient(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID inválido.");
            }

            var existingClient = _clientRepository.GetClientById(id);
            if (existingClient == null)
            {
                return NotFound("Cliente não encontrado.");
            }

            _clientRepository.DeleteClient(id);

            return NoContent();
        }

    }
}
