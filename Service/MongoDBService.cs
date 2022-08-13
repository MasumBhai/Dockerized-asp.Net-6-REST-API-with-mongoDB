using DotNet_6_REST_API_with_mongoDB.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DotNet_6_REST_API_with_mongoDB.Service
{
    public class MongoDBService
    {
        public async Task<List<Playlist>> GetAsync() { }
        public async Task CreateAsync(Playlist playlist) { }
        public async Task AddToPlaylistAsync(string id, string movieId) { }
        public async Task DeleteAsync(string id) { }

        private readonly IMongoCollection<Playlist> _playlistCollection;
        public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _playlistCollection = database.GetCollection<Playlist>(mongoDBSettings.Value.CollectionName);
        }
    }
}
