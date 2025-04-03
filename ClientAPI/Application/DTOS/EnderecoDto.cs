using ClientAPI.Domain.Models;

namespace ClientAPI.Application.DTOS
{
    public class EnderecoDto
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public string CEP { get; set; }
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public string Bairro { get; set; }
        public string Complemento { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Referencia { get; set; }

        public static explicit operator EnderecoDto(Endereco domain)
        {
            if (domain == null) return null;

            return new EnderecoDto
            {
                Id = domain.Id,
                Tipo = domain.Tipo,
                CEP = domain.CEP,
                Logradouro = domain.Logradouro,
                Numero = domain.Numero,
                Bairro = domain.Bairro,
                Complemento = domain.Complemento,
                Cidade = domain.Cidade,
                Estado = domain.Estado,
                Referencia = domain.Referencia
            };
        }
    }
}
