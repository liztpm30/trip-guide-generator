using System;
using trip_guide_generator.Model;

namespace trip_guide_generator.Services
{
    public interface ICosmosDBService
    {
        Task<User> GetUserByUserNameAsync(string userName);
        Task<User> GetUserByIdAsync(string id);
        Task AddUserAsync(User user);
        Task UpdateUserAsync(string id, User item);
        Task DeleteUserAsync(string id);
    }
}

