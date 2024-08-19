using api.tags.Config;
using api.tags.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace api.tags.ContextBase
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;
        private readonly IMongoClient _client;
        public MongoDbContext(IOptions<ConnectionMongo> connectionMongo)
        {
            var mongoClient = new MongoClient(
                              connectionString: connectionMongo.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(
                connectionMongo.Value.Database);

            _database = mongoDatabase;
            _client = mongoClient;
        }
        public IMongoCollection<WipTagModel> WipTags
        {
            get
            {
                return _database.GetCollection<WipTagModel>("wiptags");
            }
            private set { }
        }
    }
}
