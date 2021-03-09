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

        [BsonElement("images")]
        public byte[][] Images { get; set; }

        [BsonElement("owners")]
        public string Owners { get; set; }

        [BsonElement("is_rental")]
        public bool IsRental { get; set; }

        [BsonElement("building_type")]
        public string BuildingType { get; set; }

        [BsonElement("num_rooms")]
        public int NumRooms{ get; set; }

        [BsonElement("num_baths")]
        public int NumBaths { get; set; }
    }
}
