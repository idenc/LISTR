using System;
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
        private static readonly IMongoCollection<BsonDocument> collection = db.GetCollection<BsonDocument>("Houses");

        public void ReadAllDocuments()
        {
            List<BsonDocument> list = collection.AsQueryable().ToList<BsonDocument>();
        }

        public MainWindow()
        {
            InitializeComponent();
            ReadAllDocuments();
        }

        private void Media_Ended(object sender, RoutedEventArgs e)
        {
            backgroundVideo.Position = TimeSpan.FromMilliseconds(1);
        }
    }
}
