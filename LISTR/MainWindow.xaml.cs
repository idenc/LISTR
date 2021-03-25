using System.Collections.Generic;
using System.Windows;
using MongoDB.Bson;
using MongoDB.Driver;

namespace LISTR
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly MongoClient client = new MongoClient("mongodb+srv://dbUser:4nBYc8Am2MtD2FJ@cluster0.ujyt6.mongodb.net");
        private static readonly IMongoDatabase db = client.GetDatabase("LISTR");
        public static IMongoCollection<House> houses = db.GetCollection<House>("Houses");
        public static IMongoCollection<BsonDocument> accounts = db.GetCollection<BsonDocument>("Accounts");

        public void ReadAllDocuments()
        {
            List<House> list = houses.AsQueryable().ToList();
        }

        public MainWindow()
        {
            InitializeComponent();
            ReadAllDocuments();
        }
    }
}
