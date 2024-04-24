using AnthonyTravelPortal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnthonyTravelPortal.ApplicationLayer.Common.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<List<User>> GetUserListAsync();

        Task<User> GetUserByIdAsync(int ID);

        Task<User> GetUserByUser_IDAsync(string user_ID);

        Task<User> InsertUserAsync(User user);

        Task UpdateUserAsync(User user);

        Task DeleteUserAsync(User user);

    }
}
