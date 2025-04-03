using ClientAPI.Application.DTOS;
using ErrorOr;
using System.Collections.Generic;

namespace ClientAPI.Application.Interfaces
{
    public interface IClientService
    {
        List<ClienteDto> GetAllClients();

        ErrorOr<ClienteDto> GetClientById(int id);

        ErrorOr<ClienteDto> AddClient(ClienteDto clientDto);

        ErrorOr<ClienteDto> UpdateClient(ClienteDto updatedClientDto);

        ErrorOr<bool> DeleteClient(int id);
    }
}
