using ClientAPI.Domain.Models;
using ClientAPI.Models;

namespace ClientAPI.Application.DTOS
{
    public class ClienteDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public List<ContatoDto> Contatos { get; set; } = new List<ContatoDto>();
        public List<EnderecoDto> Enderecos { get; set; } = new List<EnderecoDto>();

        public static explicit operator ClienteDto(Cliente domain)
        {
            if (domain == null) return null;

            return new ClienteDto
            {
                Id = domain.Id,
                Nome = domain.Nome,
                Email = domain.Email,
                CPF = domain.CPF,
                RG = domain.RG,
                Contatos = domain.Contatos.Select(c => (ContatoDto)c).ToList(),
                Enderecos = domain.Enderecos.Select(e => (EnderecoDto)e).ToList(),
            };
        }
    }

}
