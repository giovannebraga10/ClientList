using ClientAPI.Models;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;

namespace ClientAPI.Validators
{
    public static partial class ClientValidator
    {
        public static List<string> Validate(Cliente cliente)
        {
            var errors = new List<string>();

            if (string.IsNullOrWhiteSpace(cliente.Nome))
                errors.Add("O nome é obrigatório.");

            if (string.IsNullOrWhiteSpace(cliente.Email) || !IsValidEmail(cliente.Email))
                errors.Add("E-mail inválido.");

            if (string.IsNullOrWhiteSpace(cliente.CPF) || !IsValidCPF(cliente.CPF))
                errors.Add("CPF inválido. Formato correto: 000.000.000-00");

            if (string.IsNullOrWhiteSpace(cliente.RG) || !IsValidRG(cliente.RG))
                errors.Add("RG inválido. Formato correto: 12.345.678-9");

            if (cliente.Enderecos != null)
            {
                foreach (var endereco in cliente.Enderecos)
                {
                    if (string.IsNullOrWhiteSpace(endereco.CEP) || !IsValidCEP(endereco.CEP))
                        errors.Add("CEP inválido. Formato correto: 12345-678");

                    if (string.IsNullOrWhiteSpace(endereco.Logradouro))
                        errors.Add("O logradouro é obrigatório.");

                    if (endereco.Numero <= 0)
                        errors.Add("O número deve ser maior que zero.");
                }
            }

            if (cliente.Contatos != null)
            {
                foreach (var contato in cliente.Contatos)
                {
                    if (string.IsNullOrWhiteSpace(contato.Tipo) || !new[] { "Residencial", "Comercial", "Celular" }.Contains(contato.Tipo))
                        errors.Add("Tipo de contato inválido. Use: Residencial, Comercial ou Celular.");

                    if (contato.DDD < 11 || contato.DDD > 99)
                        errors.Add("O DDD deve ter 2 dígitos.");

                    if (contato.Telefone < 10000000)
                        errors.Add("O telefone deve ter pelo menos 8 dígitos.");
                }
            }

            return errors;
        }

        [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
        private static partial Regex EmailRegex();

        [GeneratedRegex(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$")]
        private static partial Regex CPFRegex();

        [GeneratedRegex(@"^\d{2}\.\d{3}\.\d{3}-\d{1}$")]
        private static partial Regex RGRegex();

        [GeneratedRegex(@"^\d{5}-\d{3}$")]
        private static partial Regex CEPRegex();

        private static bool IsValidEmail(string email) => EmailRegex().IsMatch(email);
        private static bool IsValidCPF(string cpf) => CPFRegex().IsMatch(cpf);
        private static bool IsValidRG(string rg) => RGRegex().IsMatch(rg);
        private static bool IsValidCEP(string cep) => CEPRegex().IsMatch(cep);
    }
}
