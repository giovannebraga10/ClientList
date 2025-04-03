using ClientAPI.Domain.Models;
using ClientAPI.Domain.Shared.Errors;
using ErrorOr;
using System.Text.RegularExpressions;

public static class ClientValidator
{
    public static ErrorOr<string> ValidateName(string nome) =>
        string.IsNullOrWhiteSpace(nome) ? ValidationErrors.NameIsRequired : nome;

    public static ErrorOr<string> ValidateEmail(string email) =>
        !IsValid(email, EmailRegex) ? ValidationErrors.InvalidEmail : email;

    public static ErrorOr<string> ValidateCPF(string cpf) =>
        !IsValid(cpf, CPFRegex) ? ValidationErrors.InvalidCPF : cpf;

    public static ErrorOr<string> ValidateRG(string rg) =>
        !IsValid(rg, RGRegex) ? ValidationErrors.InvalidRG : rg;

    public static ErrorOr<Endereco> ValidateAddress(Endereco endereco)
    {
        if (!IsValid(endereco.CEP, CEPRegex))
            return ValidationErrors.InvalidCEP;

        if (string.IsNullOrWhiteSpace(endereco.Logradouro))
            return ValidationErrors.AddressIsRequired;

        if (endereco.Numero <= 0)
            return ValidationErrors.InvalidAddressNumber;

        return endereco;
    }

    public static ErrorOr<Contato> ValidateContact(Contato contato)
    {
        if (string.IsNullOrWhiteSpace(contato.Tipo) || !new[] { "Residencial", "Comercial", "Celular" }.Contains(contato.Tipo))
            return ValidationErrors.InvalidContactType;

        if (contato.DDD < 11 || contato.DDD > 99)
            return ValidationErrors.InvalidDDD;

        if (contato.Telefone < 10000000)
            return ValidationErrors.InvalidPhoneNumber;

        return contato;
    }

    private static bool IsValid(string value, Regex regex) => regex.IsMatch(value);

    private static readonly Regex EmailRegex = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
    private static readonly Regex CPFRegex = new(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$");
    private static readonly Regex RGRegex = new(@"^\d{2}\.\d{3}\.\d{3}-\d{1}$");
    private static readonly Regex CEPRegex = new(@"^\d{5}-\d{3}$");
}
