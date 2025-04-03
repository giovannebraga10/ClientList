using ClientAPI.Domain.Models;
using ErrorOr;
using System.Runtime.CompilerServices;

namespace ClientAPI.Models
{
    public class Cliente
    {
        public int Id { get; private set; }
        public string Nome { get; private set; }
        public string Email { get; private set; }
        public string CPF { get; private set; }
        public string RG { get; private set; }
        public List<Contato> Contatos { get; private set; } = new List<Contato>();
        public List<Endereco> Enderecos { get; private set; } = new List<Endereco>();

        public static ErrorOr<Cliente> Create(int id, string nome, string email, string cpf, string rg)
        {
            var cliente = new Cliente();

            var nomeResult = cliente.ValidateName(nome);
            var emailResult = cliente.ValidateEmail(email);
            var cpfResult = cliente.ValidateCPF(cpf);
            var rgResult = cliente.ValidateRG(rg);

            if (nomeResult.IsError || emailResult.IsError || cpfResult.IsError || rgResult.IsError)
            {
                return nomeResult.Errors
                    .Concat(emailResult.Errors)
                    .Concat(cpfResult.Errors)
                    .Concat(rgResult.Errors)
                    .ToList();
            }

            cliente.Id = id;
            cliente.Nome = nome;
            cliente.Email = email;
            cliente.CPF = cpf;
            cliente.RG = rg;

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

            if (validationResult.IsError)
            {
                return validationResult.Errors;
            }

            return this;
        }

        public ErrorOr<Cliente> ValidateEmail(string email)
        {
            var validationResult = ClientValidator.ValidateEmail(email);

            if (validationResult.IsError)
            {
                return validationResult.Errors;
            }

            return this;
        }

        public ErrorOr<Cliente> ValidateCPF(string cpf)
        {
            var validationResult = ClientValidator.ValidateCPF(cpf);

            if (validationResult.IsError)
            {
                return validationResult.Errors;
            }

            return this;
        }

        public ErrorOr<Cliente> ValidateRG(string rg)
        {
            var validationResult = ClientValidator.ValidateRG(rg);

            if (validationResult.IsError)
            {
                return validationResult.Errors;
            }

            return this;
        }

        public ErrorOr<Cliente> ValidateAddress(Endereco endereco)
        {
            var validationResult = ClientValidator.ValidateAddress(endereco);

            if (validationResult.IsError)
            {
                return validationResult.Errors;
            }

            return this;
        }

        public ErrorOr<Cliente> ValidateContact(Contato contato)
        {
            var validationResult = ClientValidator.ValidateContact(contato);

            if (validationResult.IsError)
            {
                return validationResult.Errors;
            }

            return this;
        }
    }
}
