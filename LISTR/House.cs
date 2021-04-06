using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LISTR
{
    [BsonIgnoreExtraElements]
    public class House
    {
        //private string _id;
        //private string _description;
        //private decimal _price;
        //private string _address;
        //private string _province;
        //private string _city;
        //private string _postalcode;
        //private byte[][] _images;
        //private string _owners;
        //private bool _is_rental;
        //private string _building_type;
        //private string _num_rooms;
        //private string _num_baths;
        private string _realtor;

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("description"), BsonDefaultValue("")]
        public string Description { get; set; }

        [BsonElement("price"), BsonDefaultValue(0.0)]
        public double Price { get; set; }

        [BsonElement("address"), BsonDefaultValue("")]
        public string Address { get; set; }

        [BsonElement("province"), BsonDefaultValue("")]
        public string Province { get; set; }

        [BsonElement("city"), BsonDefaultValue("")]
        public string City { get; set; }

        [BsonElement("postal_code"), BsonDefaultValue("")]
        public string PostalCode { get; set; }

        [BsonElement("images"), BsonDefaultValue(null)]
        public byte[][] Images { get; set; }

        [BsonElement("owners"), BsonDefaultValue("")]
        public string Owners { get; set; }

        [BsonElement("is_rental"), BsonDefaultValue(false)]
        public bool IsRental { get; set; }

        [BsonElement("building_type"), BsonDefaultValue("")]
        public string BuildingType { get; set; }

        [BsonElement("num_rooms"), BsonDefaultValue("")]
        public string NumRooms { get; set; }

        [BsonElement("num_baths"), BsonDefaultValue("")]
        public string NumBaths { get; set; }

        [BsonElement("realtor"), BsonDefaultValue("")]
        public string Realtor
        {
            get => _realtor; set
            {
                if (value != null && !IsStringEmpty(value))
                {
                    _realtor = value;
                }
            }
        }

        [BsonElement("area"), BsonDefaultValue(0.0)]
        public double Area { get; set; }

        [BsonElement("tags"), BsonDefaultValue(null)]
        public string[] Tags { get; set; }

        private bool IsStringEmpty(string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }
    }
}