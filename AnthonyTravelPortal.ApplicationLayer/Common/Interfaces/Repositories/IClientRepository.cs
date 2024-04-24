using AnthonyTravelPortal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnthonyTravelPortal.ApplicationLayer.Common.Interfaces.Repositories
{
    public interface IClientRepository
    {
        Task<List<Client>> GetClientListAsync();

        Task<Client> GetClientByIdAsync(int clientId);

        //Task<List<Client>> GetClientByUserIdAsync(string userId);

        Task<int> InsertClientAsync(Client client);

        Task UpdateClientAsync(Client client);

        Task DeleteClientAsync(Client client);
    }
}
