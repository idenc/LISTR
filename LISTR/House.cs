using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LISTR
{
    [BsonIgnoreExtraElements]
    public class House
    {
        private string _id;
        private string _description;
        private decimal _price;
        private string _address;
        private string _province;
        private string _city;
        private string _postalcode;
        private byte[][] _images;
        private string _owners;
        private bool _is_rental;
        private string _building_type;
        private string _num_rooms;
        private string _num_baths;
        private string _realtor;

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

        [BsonElement("postal_code"), BsonDefaultValue(null)]
        public string PostalCode { get; set; }

        [BsonElement("images"), BsonDefaultValue(null)]
        public byte[][] Images { get; set; }

        [BsonElement("owners"), BsonDefaultValue(null)]
        public string Owners { get; set; }

        [BsonElement("is_rental"), BsonDefaultValue(null)]
        public bool IsRental { get; set; }

        [BsonElement("building_type"), BsonDefaultValue(null)]
        public string BuildingType { get; set; }

        [BsonElement("num_rooms"), BsonDefaultValue(null)]
        public string NumRooms { get; set; }

        [BsonElement("num_baths"), BsonDefaultValue(null)]
        public string NumBaths { get; set; }

        [BsonElement("realtor"), BsonDefaultValue(null)]
        public string Realtor
        {
            get => _realtor; set
            {
                if (!isStringEmpty(value))
                {
                    _realtor = value;
                }
            }
        }

        private bool isStringEmpty(string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }
    }
}