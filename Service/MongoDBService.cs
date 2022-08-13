using DotNet_6_REST_API_with_mongoDB.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DotNet_6_REST_API_with_mongoDB.Service
{
    public class MongoDBService
    {
        private readonly IMongoCollection<Our_Clients> _Our_ClientsCollection;
        public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _Our_ClientsCollection = database.GetCollection<Our_Clients>(mongoDBSettings.Value.CollectionName);
        }

        public async Task<List<Our_Clients>> GetAsync()
        {
            return await _Our_ClientsCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task CreateAsync(Our_Clients clients)
        {
            await _Our_ClientsCollection.InsertOneAsync(clients);
            return;
        }

        public async Task DeleteAsync(string id)
        {
            FilterDefinition<Our_Clients> filter = Builders<Our_Clients>.Filter.Eq("Id", ObjectId.Parse(id));
            await _Our_ClientsCollection.DeleteOneAsync(filter);
            return;
        }

        public async Task AddToClientsAsync(string id, string password)
        {
            FilterDefinition<Our_Clients> filter = Builders<Our_Clients>.Filter.Eq("Id", ObjectId.Parse(id));
            //UpdateDefinition<Our_Clients> update = Builders<Our_Clients>.Update.AddToSet<string>("password", password);
            UpdateDefinition<Our_Clients> update = Builders<Our_Clients>.Update.Set<string>("password", password);
            await _Our_ClientsCollection.UpdateOneAsync(filter, update);
            return;
        }
    }
}
