using Microsoft.Azure.Cosmos;
using trip_guide_generator.Model;

namespace trip_guide_generator.Services
{
    public class CosmosDBService : ICosmosDBService
    {
        private Container _container;

        public CosmosDBService(CosmosClient dbClient, string dbName, string containeName)
        {
            this._container = dbClient.GetContainer(dbName, containeName);
        }

        public async Task AddUserAsync(AppUser user)
        {
            await this._container.CreateItemAsync<AppUser>(user, new PartitionKey(user.Id));
        }

        public async Task DeleteUserAsync(string id)
        {
            await this._container.DeleteItemAsync<AppUser>(id, new PartitionKey(id));
        }

        public async Task<AppUser> GetUserByIdAsync(string id)
        {
            try
            {
                ItemResponse<AppUser> response = await this._container.ReadItemAsync<AppUser>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

        }

        public async Task<AppUser> GetUserByUserNameAsync(string userName)
        {
            var queryString = $"SELECT * FROM Users u WHERE u.userName = \"{userName}\"";
            var query = this._container.GetItemQueryIterator<AppUser>(new QueryDefinition(queryString));
            List<AppUser> results = new List<AppUser>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results.FirstOrDefault();

        }

        public async Task UpdateUserAsync(string id, AppUser user)
        {
            await this._container.UpsertItemAsync<AppUser>(user, new PartitionKey(id));
        }
    }
}

