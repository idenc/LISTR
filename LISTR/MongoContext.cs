using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace LISTR
{
    public interface IMongoDBContext
    {
        IMongoCollection<House> GetCollection<House>(string name);
    }

    public class MongoDBContext : IMongoDBContext
    {
        private IMongoDatabase _db { get; set; }
        private IMongoClient _mongoClient { get; set; }
        public IClientSessionHandle Session { get; set; }
        public MongoDBContext()
        {
            _mongoClient = new MongoClient("mongodb+srv://dbUser:4nBYc8Am2MtD2FJ@cluster0.ujyt6.mongodb.net/myFirstDatabase?retryWrites=true&w=majority");

            _db = _mongoClient.GetDatabase("LISTR");
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }
            return _db.GetCollection<T>(name);
        }
    }

}