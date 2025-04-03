using ClientAPI.Application.DTOS;
using ClientAPI.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ClientAPI.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpPost]
        public IActionResult CreateClient([FromBody] ClienteRequestDto requestDto)
        {
            var clientDto = new ClienteDto
            {

                Nome = requestDto.Nome,
                Email = requestDto.Email,
                CPF = requestDto.CPF,
                RG = requestDto.RG
            };

            var result = _clientService.AddClient(clientDto);

            if (result.IsError) return BadRequest(result.Errors);

            return CreatedAtAction(nameof(GetClientById), new { id = result.Value.Id }, result.Value);
        }

        [HttpGet("{id}")]
        public IActionResult GetClientById(int id)
        {
            var result = _clientService.GetClientById(id);

            if (result.IsError) return NotFound(result.Errors);

            return Ok(result.Value);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateClient(int id, [FromBody] ClienteRequestDto requestDto)
        {
            var updatedClientDto = new ClienteDto
            {
                Id = id,
                Nome = requestDto.Nome,
                Email = requestDto.Email,
                CPF = requestDto.CPF,
                RG = requestDto.RG
            };

            var result = _clientService.UpdateClient(updatedClientDto);

            if (result.IsError) return BadRequest(result.Errors);

            return Ok(result.Value);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteClient(int id)
        {
            var result = _clientService.DeleteClient(id);

            if (result.IsError) return BadRequest(result.Errors);

            return NoContent();
        }
    }
}
