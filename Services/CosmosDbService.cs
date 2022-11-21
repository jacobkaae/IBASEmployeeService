using System;
namespace IBASEmployeeService.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using IBASEmployeeService.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Azure.Cosmos;
    using Microsoft.Azure.Cosmos.Fluent;
    using Microsoft.Extensions.Configuration;

    public class CosmosDbService
    {
        private Container _container;

        private CosmosClient dbClient;

        public CosmosDbService(IConfiguration configuration)
        {
            var databaseName = configuration["CosmosDb:DatabaseName"];
            var containerName = configuration["CosmosDb:ContainerName"];
            var account = configuration["CosmosDb:Account"];
            var key = configuration["CosmosDb:Key"];

            dbClient = new CosmosClient(account, key);
            this._container = dbClient.GetContainer(databaseName, containerName);
        }

        public async Task<List<Henvendelse>> GetHenvendelserAsync()
        {
            using FeedIterator<Henvendelse> feed = _container.GetItemQueryIterator<Henvendelse>(
             queryText: "SELECT * FROM C");

            List<Henvendelse> list = new List<Henvendelse>();

            while (feed.HasMoreResults)
            {
                FeedResponse<Henvendelse> response = await feed.ReadNextAsync();

                // Iterate query results
                foreach (Henvendelse item in response)
                {
                    list.Add(item);
                }
            }

            return list;

        }

        public async void PostHenvendelse(HenvendelseDTO test)
        {
            Henvendelse nyHenvendelse = new Henvendelse { Id = Guid.NewGuid().ToString(), Beskrivelse = test.Beskrivelse, Bruger = test.Bruger, Dato = test.Dato, Kategori = test.Kategori };

            var tempPartitionKey = new PartitionKey(test.Kategori);

            ItemResponse<Henvendelse> henvendelseResponse = await _container.CreateItemAsync<Henvendelse>(nyHenvendelse, tempPartitionKey);
        }
    }
}

