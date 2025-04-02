using System.Text.Json;
using ClientAPI.Models;

namespace ClientAPI.Repositories
{
    public class ClientRepository
    {
        private readonly string _filePath = "Data/clients.json";

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

        public void AddClient(Cliente client)
        {
            var clients = GetAllClients();
            client.Id = clients.Any() ? clients.Max(c => c.Id) + 1 : 1;
            clients.Add(client);
            SaveAllClients(clients);
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
