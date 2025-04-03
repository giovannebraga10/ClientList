
using ErrorOr;
namespace ClientAPI.Domain.Shared.Errors;

public static class ValidationErrors
{
    public static Error NameIsRequired { get; } =
        Error.Validation(
            code: "Validation.NameIsRequired",
            description: "O nome é obrigatório.");

    public static Error InvalidEmail { get; } =
        Error.Validation(
            code: "Validation.InvalidEmail",
            description: "E-mail inválido.");

    public static Error InvalidCPF { get; } =
        Error.Validation(
            code: "Validation.InvalidCPF",
            description: "CPF inválido. Formato: 000.000.000-00");

    public static Error InvalidRG { get; } =
        Error.Validation(
            code: "Validation.InvalidRG",
            description: "RG inválido. Formato: 12.345.678-9");

    public static Error InvalidCEP { get; } =
        Error.Validation(
            code: "Validation.InvalidCEP",
            description: "CEP inválido. Formato: 12345-678");

    public static Error AddressIsRequired { get; } =
        Error.Validation(
            code: "Validation.AddressIsRequired",
            description: "O logradouro é obrigatório.");

    public static Error InvalidAddressNumber { get; } =
        Error.Validation(
            code: "Validation.InvalidAddressNumber",
            description: "O número deve ser maior que zero.");

    public static Error InvalidContactType { get; } =
        Error.Validation(
            code: "Validation.InvalidContactType",
            description: "Tipo de contato inválido. Use: Residencial, Comercial ou Celular.");

    public static Error InvalidDDD { get; } =
        Error.Validation(
            code: "Validation.InvalidDDD",
            description: "O DDD deve ter 2 dígitos.");

    public static Error InvalidPhoneNumber { get; } =
        Error.Validation(
            code: "Validation.InvalidPhoneNumber",
            description: "O telefone deve ter pelo menos 8 dígitos.");
}
