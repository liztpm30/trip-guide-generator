using System;
using trip_guide_generator.Model;

namespace trip_guide_generator.Services
{
    public interface ICosmosDBService
    {
        Task<AppUser> GetUserByUserNameAsync(string userName);
        Task<AppUser> GetUserByIdAsync(string id);
        Task AddUserAsync(AppUser user);
        Task UpdateUserAsync(string id, AppUser item);
        Task DeleteUserAsync(string id);
    }
}

