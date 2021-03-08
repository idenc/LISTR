using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LISTR
{
    public class House
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }

        [BsonElement("price")]
        public decimal Price { get; set; }

        [BsonElement("address")]
        public string Address { get; set; }

        [BsonElement("province")]
        public string Province { get; set; }

        [BsonElement("city")]
        public string City { get; set; }
    }
}
