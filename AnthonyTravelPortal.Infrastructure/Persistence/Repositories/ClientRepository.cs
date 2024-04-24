using AnthonyTravelPortal.ApplicationLayer.Common.Interfaces.Repositories;
using AnthonyTravelPortal.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnthonyTravelPortal.Infrastructure.Persistence.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly IRepositoryAsync<Client> _repository;

        public ClientRepository(IRepositoryAsync<Client> repository)
        {
            _repository = repository;
        }
        private IQueryable<Client> Clients => _repository.Entities;

        public async Task DeleteClientAsync(Client client)
        {
            await _repository.DeleteAsync(client);
        }

        public async Task<Client> GetClientByIdAsync(int clientId)
        {
            return await _repository.Entities
               .FirstOrDefaultAsync(c => c.Client_ID == clientId);
        }

        //public async Task<List<Client>> GetClientByUserIdAsync(string userId)
        //{
        //    return await _repository.Entities.Where(x=>x.UserId == userId).ToListAsync(); 
        //}

        public async Task<List<Client>> GetClientListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<int> InsertClientAsync(Client client)
        {
            await _repository.AddAsync(client);
            return client.Client_ID;
        }

        public async Task UpdateClientAsync(Client client)
        {
            await _repository.UpdateAsync(client);
        }
    }
}
