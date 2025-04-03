using ClientAPI.Models;
using ErrorOr;

namespace ClientAPI.Domain.Interfaces
{
    public interface IClientRepository
    {
        List<Cliente> GetAllClients();
        Cliente? GetClientById(int id);
        ErrorOr<Cliente> AddClient(Cliente client);
        void UpdateClient(Cliente updatedClient);
        void DeleteClient(int id);
    }
}
