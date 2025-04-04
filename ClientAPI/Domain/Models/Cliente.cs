using ClientAPI.Application.DTOS;
using ClientAPI.Domain.Models;
using ErrorOr;

namespace ClientAPI.Models
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public string RG { get; set; } = string.Empty;
        public List<Contato> Contatos { get; set; } = new();
        public List<Endereco> Enderecos { get; set; } = new();

        public static ErrorOr<Cliente> Create(int id, string nome, string email, string cpf, string rg, List<ContatoDto> contato, List<EnderecoDto> endereco)
        {
            var cliente = new Cliente();

            var nomeResult = cliente.ValidateName(nome);
            var emailResult = cliente.ValidateEmail(email);
            var cpfResult = cliente.ValidateCPF(cpf);
            var rgResult = cliente.ValidateRG(rg);

            var allErrors = new List<Error>();

            if (nomeResult.IsError) allErrors.AddRange(nomeResult.Errors);
            if (emailResult.IsError) allErrors.AddRange(emailResult.Errors);
            if (cpfResult.IsError) allErrors.AddRange(cpfResult.Errors);
            if (rgResult.IsError) allErrors.AddRange(rgResult.Errors);

            foreach (var c in contato)
            {
                var contatoResult = cliente.ValidateContact(c.ToEntity());
                if (contatoResult.IsError) allErrors.AddRange(contatoResult.Errors);
            }

            foreach (var e in endereco)
            {
                var enderecoResult = cliente.ValidateAddress(e.ToEntity());
                if (enderecoResult.IsError) allErrors.AddRange(enderecoResult.Errors);
            }

            if (allErrors.Any())
                return allErrors;

            cliente.Id = id;
            cliente.Nome = nome;
            cliente.Email = email;
            cliente.CPF = cpf;
            cliente.RG = rg;
            cliente.Contatos = contato.Select(c => c.ToEntity()).ToList();
            cliente.Enderecos = endereco.Select(e => e.ToEntity()).ToList();

            return cliente;
        }

        public ErrorOr<Cliente> AddContact(Contato contato)
        {
            var contatoResult = ValidateContact(contato);

            if (contatoResult.IsError)
                return contatoResult.Errors;

            Contatos.Add(contato);
            return this;
        }

        public ErrorOr<Cliente> AddAddress(Endereco endereco)
        {
            var enderecoResult = ValidateAddress(endereco);

            if (enderecoResult.IsError)
                return enderecoResult.Errors;

            Enderecos.Add(endereco);
            return this;
        }

        public ErrorOr<Cliente> ValidateName(string nome)
        {
            var validationResult = ClientValidator.ValidateName(nome);
            return validationResult.IsError ? validationResult.Errors : this;
        }

        public ErrorOr<Cliente> ValidateEmail(string email)
        {
            var validationResult = ClientValidator.ValidateEmail(email);
            return validationResult.IsError ? validationResult.Errors : this;
        }

        public ErrorOr<Cliente> ValidateCPF(string cpf)
        {
            var validationResult = ClientValidator.ValidateCPF(cpf);
            return validationResult.IsError ? validationResult.Errors : this;
        }

        public ErrorOr<Cliente> ValidateRG(string rg)
        {
            var validationResult = ClientValidator.ValidateRG(rg);
            return validationResult.IsError ? validationResult.Errors : this;
        }

        public ErrorOr<Cliente> ValidateAddress(Endereco endereco)
        {
            var validationResult = ClientValidator.ValidateAddress(endereco);
            return validationResult.IsError ? validationResult.Errors : this;
        }

        public ErrorOr<Cliente> ValidateContact(Contato contato)
        {
            var validationResult = ClientValidator.ValidateContact(contato);
            return validationResult.IsError ? validationResult.Errors : this;
        }
    }

    public static class DtoExtensions
    {
        public static Contato ToEntity(this ContatoDto dto)
        {
            return new Contato
            {
                Tipo = dto.Tipo,
                DDD = dto.DDD,
                Telefone = dto.Telefone
            };
        }

        public static Endereco ToEntity(this EnderecoDto dto)
        {
            return new Endereco
            {
                Id = dto.Id,
                Tipo = dto.Tipo,
                CEP = dto.CEP,
                Logradouro = dto.Logradouro,
                Numero = dto.Numero,
                Bairro = dto.Bairro,
                Complemento = dto.Complemento,
                Cidade = dto.Cidade,
                Estado = dto.Estado,
                Referencia = dto.Referencia
            };
        }
    }
}
