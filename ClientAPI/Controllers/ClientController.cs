using Microsoft.AspNetCore.Mvc;
using ClientAPI.Repositories;
using ClientAPI.Models;
using System;

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
            try
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
            catch (Exception ex)
            {
                return StatusCode(500, "Erro interno do servidor: " + ex.Message);
            }
        }

        [HttpPost("criar")]
        public ActionResult<Cliente> CreateClient([FromBody] Cliente cliente)
        {
            try
            {
                if (cliente == null)
                    return BadRequest("Dados inválidos.");

                var existingClient = _clientRepository.GetAllClients()
                    .FirstOrDefault(c => c.CPF == cliente.CPF || c.Email == cliente.Email);

                if (existingClient != null)
                    return Conflict("Já existe um cliente cadastrado com esse CPF ou Email.");

                _clientRepository.AddClient(cliente);

                return CreatedAtAction(nameof(GetClients), new { id = cliente.Id }, cliente);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao criar cliente: " + ex.Message);
            }
        }

        [HttpPut("atualizar/{id}")]
        public ActionResult<Cliente> UpdateClient(int id, [FromBody] Cliente cliente)
        {
            try
            {
                if (cliente == null || id <= 0)
                    return BadRequest("Dados inválidos.");

                var existingClient = _clientRepository.GetClientById(id);
                if (existingClient == null)
                    return NotFound("Cliente não encontrado.");

                existingClient.Nome = cliente.Nome;
                existingClient.Email = cliente.Email;
                existingClient.CPF = cliente.CPF;
                existingClient.RG = cliente.RG;

                _clientRepository.UpdateClient(existingClient);

                return Ok(existingClient);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao atualizar cliente: " + ex.Message);
            }
        }

        [HttpDelete("deletar/{id}")]
        public ActionResult DeleteClient(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest("ID inválido.");

                var existingClient = _clientRepository.GetClientById(id);
                if (existingClient == null) 
                    return NotFound("Cliente não encontrado. ");

                _clientRepository.DeleteClient(id);

                return Ok("Cliente removido com sucesso. ");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Erro ao deletar cliente: " + ex.Message);
            }
        }
    }
}
