using ClientAPI.Application.DTOS;
using ClientAPI.Domain.Models;
using ClientAPI.Models;
using ErrorOr;

namespace ClientAPI.Domain.Interfaces
{
    public interface IClientRepository
    {
        List<Cliente> GetAllClients();
        Cliente? GetClientById(int id);
        ErrorOr<Cliente> AddClient(string nome, string email, string cpf, string rg, List<ContatoDto> contato, List<EnderecoDto> endereco);
        void UpdateClient(Cliente updatedClient);
        void DeleteClient(int id);
    }
}
