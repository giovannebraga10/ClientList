using System.Collections.Generic;

namespace ClientAPI.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string CPF {  get; set; }
        public string RG { get; set; }
        public List<Contato> Contatos { get; set; } = new List<Contato>();
        public List<Endereco> Enderecos { get; set; } = new List<Endereco>();
    }
}