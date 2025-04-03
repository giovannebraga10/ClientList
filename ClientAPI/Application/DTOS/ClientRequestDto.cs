namespace ClientAPI.Application.DTOS
{
    public class ClienteRequestDto
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string CPF { get; set; }
        public string RG { get; set; }
        public List<ContatoDto> Contatos { get; set; } = new();
        public List<EnderecoDto> Enderecos { get; set; } = new();
    }
}
