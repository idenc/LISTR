using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LISTR
{
    [BsonIgnoreExtraElements]
    public class House
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("description"), BsonDefaultValue(null)]
        public string Description { get; set; }

        [BsonElement("price"), BsonDefaultValue(null)]
        public decimal Price { get; set; }

        [BsonElement("address"), BsonDefaultValue(null)]
        public string Address { get; set; }

        [BsonElement("province"), BsonDefaultValue(null)]
        public string Province { get; set; }

        [BsonElement("city"), BsonDefaultValue(null)]
        public string City { get; set; }

        [BsonElement("images"), BsonDefaultValue(null)]
        public byte[][] Images { get; set; }

        [BsonElement("owners"), BsonDefaultValue(null)]
        public string Owners { get; set; }

        [BsonElement("is_rental"), BsonDefaultValue(null)]
        public bool IsRental { get; set; }

        [BsonElement("building_type"), BsonDefaultValue(null)]
        public string BuildingType { get; set; }

        [BsonElement("num_rooms"), BsonDefaultValue(null)]
        public int NumRooms { get; set; }

        [BsonElement("num_baths"), BsonDefaultValue(null)]
        public int NumBaths { get; set; }

        [BsonElement("realtor"), BsonDefaultValue(null)]
        public string Realtor { get; set; }
    }
}