using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using MongoDB.Driver;

namespace LISTR
{
    /// <summary>
    /// Interaction logic for RealtorListings.xaml
    /// </summary>
    public partial class RealtorListings : Page
    {
        public ObservableCollection<House> houses = new ObservableCollection<House>(MainWindow.houses.AsQueryable().ToList());
        private readonly MainWindow mainWindow;

        public RealtorListings()
        {
            mainWindow = (MainWindow)Application.Current.MainWindow;
            InitializeComponent();
        }

        private void AddListingClick(object sender, RoutedEventArgs e)
        {
            mainWindow.Main.Navigate(new AddListing());
        }

        private void HomeButtonClick(object sender, RoutedEventArgs e)
        {
            mainWindow.Main.Navigate(new HomePage());
        }
    }
}