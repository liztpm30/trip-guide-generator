using System;
using Microsoft.AspNetCore.Http;
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

        public async Task AddUserAsync(Model.User user)
        {
            await this._container.CreateItemAsync<Model.User>(user, new PartitionKey(user.Id));
        }

        public async Task DeleteUserAsync(string id)
        {
            await this._container.DeleteItemAsync<Model.User>(id, new PartitionKey(id));
        }

        public async Task<Model.User> GetUserByIdAsync(string id)
        {
            try
            {
                ItemResponse<Model.User> response = await this._container.ReadItemAsync<Model.User>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

        }

        public async Task<Model.User> GetUserByUserNameAsync(string userName)
        {
            var queryString = $"SELECT * FROM Users u WHERE u.userName = \"{userName}\"";
            var query = this._container.GetItemQueryIterator<Model.User>(new QueryDefinition(queryString));
            List<Model.User> results = new List<Model.User>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results.FirstOrDefault();

        }

        public async Task UpdateUserAsync(string id, Model.User user)
        {
            await this._container.UpsertItemAsync<Model.User>(user, new PartitionKey(id));
        }
    }
}

