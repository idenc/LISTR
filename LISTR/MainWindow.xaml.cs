using System;
using System.Collections.Generic;
using System.Windows;
using MongoDB.Bson;
using MongoDB.Driver;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;

namespace LISTR
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static readonly MongoClient client = new MongoClient("mongodb://dbUser:4nBYc8Am2MtD2FJ@cluster0-shard-00-00.ujyt6.mongodb.net:27017,cluster0-shard-00-01.ujyt6.mongodb.net:27017,cluster0-shard-00-02.ujyt6.mongodb.net:27017/myFirstDatabase?authSource=admin&replicaSet=atlas-fsglxp-shard-0&w=majority&readPreference=primary&appname=MongoDB%20Compass&retryWrites=true&ssl=true");
        private static readonly IMongoDatabase db = client.GetDatabase("LISTR");
        public static IMongoCollection<House> houseCollection = db.GetCollection<House>("Houses");
        public static List<House> houses = houseCollection.AsQueryable().ToList();
        public static IMongoCollection<BsonDocument> accounts = db.GetCollection<BsonDocument>("Accounts");

        public static Notifier notifier = new Notifier(cfg =>
        {
            cfg.PositionProvider = new WindowPositionProvider(
                parentWindow: Application.Current.MainWindow,
                corner: Corner.TopRight,
                offsetX: 20,
                offsetY: 20);

            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(3),
                maximumNotificationCount: MaximumNotificationCount.FromCount(5));

            cfg.Dispatcher = Application.Current.Dispatcher;
        });

        public MainWindow()
        {
            InitializeComponent();
        }
    }
}