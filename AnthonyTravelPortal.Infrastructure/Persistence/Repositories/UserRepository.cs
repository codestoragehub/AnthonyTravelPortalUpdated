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
    public class UserRepository : IUserRepository
    {
        private readonly IRepositoryAsync<User> _repository;

        public UserRepository(IRepositoryAsync<User> repository)
        {
            _repository = repository;
        }
        public async Task DeleteUserAsync(User user)
        {
            await _repository.DeleteAsync(user);
        }

        public async Task<User> GetUserByIdAsync(int ID)
        {
            return await _repository.Entities
               .FirstOrDefaultAsync(c => c.ID == ID);
        }

        public async Task<User> GetUserByUser_IDAsync(string user_ID)
        {
            return await _repository.Entities
               .FirstOrDefaultAsync(c => c.User_ID == user_ID);
        }

        public async Task<List<User>> GetUserListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }

        public async Task<User> InsertUserAsync(User user)
        {
            return await _repository.AddAsync(user);
        }

        public async Task UpdateUserAsync(User user)
        {
            await _repository.UpdateAsync(user);
        }
    }
}
