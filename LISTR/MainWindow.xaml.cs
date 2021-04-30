using System;
using System.Collections.Generic;
using System.Configuration;
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
        private static readonly MongoClient client = new MongoClient(ConfigurationManager.ConnectionStrings["LISTR"].ConnectionString);
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
            Console.WriteLine(houses[0].Id);
            InitializeComponent();
        }
    }
}