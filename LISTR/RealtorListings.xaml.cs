using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using MongoDB.Driver;

namespace LISTR
{
    /// <summary>
    /// Interaction logic for RealtorListings.xaml
    /// </summary>
    public partial class RealtorListings : Page
    {
        public static ObservableCollection<House> houses = new ObservableCollection<House>(MainWindow.houses.AsQueryable().ToList());
        private readonly MainWindow mainWindow;

        public RealtorListings()
        {
            mainWindow = (MainWindow)Application.Current.MainWindow;
            Console.WriteLine("Num Houses: " + houses.Count);
            InitializeComponent();

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(MyControl.ItemsSource);
            view.Filter = ListingFilter;
        }

        private void AddListingClick(object sender, RoutedEventArgs e)
        {
            mainWindow.Main.Navigate(new AddListing());
        }

        private void HomeButtonClick(object sender, RoutedEventArgs e)
        {
            mainWindow.Main.Navigate(new HomePage());
        }

        private void DeleteListing(object sender, RoutedEventArgs e)
        {
            string id = ((Button)sender).Tag as string;
            var deleteFilter = Builders<House>.Filter.Eq("_id", id);
            MainWindow.houseCollection.DeleteOne(deleteFilter);
            houses.Remove(houses.Where(i => i.Id == id).Single());
        }

        private bool ListingFilter(object item)
        {
            if (String.IsNullOrWhiteSpace(ListingsSearch.Text))
                return true;
            else
                return ((item as House).Address.IndexOf(ListingsSearch.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        private void ListingSearch(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(MyControl.ItemsSource).Refresh();
        }
    }

    public class PriceConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values == null)
                throw new ArgumentException("Values cannot be null.");
            if (values.Length != 2)
                throw new ArgumentException("Incorrect number of bindings (" + values.Length + ")");

            string price = values[0].ToString();
            bool isRental = (bool)values[1];

            if (isRental)
            {
                return "For Rent: " + price + " per month";
            }
            else
            {
                return "For Sale: " + price;
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}