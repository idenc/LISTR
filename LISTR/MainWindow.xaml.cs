using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Navigation;
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
        public static IMongoCollection<BsonDocument> houses = db.GetCollection<BsonDocument>("Houses");
        public static IMongoCollection<BsonDocument> accounts = db.GetCollection<BsonDocument>("Accounts");

        public void ReadAllDocuments()
        {
            List<BsonDocument> list = houses.AsQueryable().ToList<BsonDocument>();
        }

        public MainWindow()
        {
            InitializeComponent();
            ReadAllDocuments();
        }
    }
}
