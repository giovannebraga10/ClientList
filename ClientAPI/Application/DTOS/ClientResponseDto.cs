using ClientAPI.Models;

namespace ClientAPI.Application.DTOS
{
    public class ClienteResponseDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public List<ContatoDto> Contatos { get; set; } = new();
        public List<EnderecoDto> Enderecos { get; set; } = new();

        public static explicit operator ClienteResponseDto(Cliente domain)
        {
            return new ClienteResponseDto
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
