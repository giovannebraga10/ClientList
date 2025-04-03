using ClientAPI.Domain.Models;

namespace ClientAPI.Application.DTOS
{
    public class ContatoDto
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public int DDD { get; set; }
        public decimal Telefone { get; set; }

        public static explicit operator ContatoDto(Contato domain)
        {
            if (domain == null) return null;

            return new ContatoDto
            {
                Id = domain.Id,
                Tipo = domain.Tipo,
                DDD = domain.DDD,
                Telefone = domain.Telefone,
            };
        }
    }
}
