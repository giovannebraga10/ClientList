using System.Text.Json;
using ClientAPI.Domain.Models;
using ClientAPI.Domain.Interfaces;
using ClientAPI.Models;
using ErrorOr;
using ClientAPI.Application.DTOS;

namespace ClientAPI.Infrastructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly string _filePath = "Infrastructure/Data/clients.json";

        public List<Cliente> GetAllClients()
        {
            if (!File.Exists(_filePath))
            {
                return new List<Cliente>();
            }

            string json = File.ReadAllText(_filePath);
            return JsonSerializer.Deserialize<List<Cliente>>(json) ?? new List<Cliente>();
        }

        public void SaveAllClients(List<Cliente> clients)
        {
            string json = JsonSerializer.Serialize(clients, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, json);
        }

        public ErrorOr<Cliente> AddClient(string nome, string email, string cpf, string rg, List<ContatoDto> contato, List<EnderecoDto> endereco)
        {
            var clients = GetAllClients();
            int newId = clients.Any() ? clients.Max(c => c.Id) + 1 : 1;

            var result = Cliente.Create(newId, nome, email, cpf, rg, contato, endereco);

            if (result.IsError)
            {
                return result.Errors;
            }

            clients.Add(result.Value);
            SaveAllClients(clients);

            return result.Value;
        }


        public Cliente? GetClientById(int id)
        {
            return GetAllClients().FirstOrDefault(c => c.Id == id);
        }

        public void UpdateClient(Cliente updatedClient)
        {
            var clients = GetAllClients();
            var index = clients.FindIndex(c => c.Id == updatedClient.Id);
            if (index != -1)
            {
                clients[index] = updatedClient;
                SaveAllClients(clients);
            }
        }

        public void DeleteClient(int id)
        {
            var clients = GetAllClients();
            clients.RemoveAll(c => c.Id == id);
            SaveAllClients(clients);
        }
    }
}
