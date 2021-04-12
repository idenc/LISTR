using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
using MongoDB.Bson;
using MongoDB.Driver;
using ToastNotifications.Lifetime.Clear;
using ToastNotifications.Messages;

namespace LISTR
{
    /// <summary>
    /// Interaction logic for RealtorListings.xaml
    /// </summary>
    public partial class RealtorListings : Page
    {
        public static ObservableCollection<House> AllHouses = new ObservableCollection<House>(MainWindow.houses.AsQueryable().ToList());
        private ObservableCollection<House> houses;
        private readonly MainWindow mainWindow;
        private bool justPosted = false;
        private bool justUpdated = false;

        public RealtorListings(bool justPosted = false, bool justUpdated = false)
        {
            mainWindow = (MainWindow)Application.Current.MainWindow;
            Console.WriteLine("Num Houses: " + AllHouses.Count);
            AllHouses = new ObservableCollection<House>(MainWindow.houses.AsQueryable().ToList());
            houses = new ObservableCollection<House>(AllHouses.Take(AllHouses.Count - 1));
            InitializeComponent();

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(MyControl.ItemsSource);
            view.Filter = ListingFilter;
            this.justPosted = justPosted;
            this.justUpdated = justUpdated;
        }

        private void AddListingClick(object sender, RoutedEventArgs e)
        {
            MainWindow.notifier.ClearMessages(new ClearAll());
            mainWindow.Main.Navigate(new AddListing());
        }

        private void HomeButtonClick(object sender, RoutedEventArgs e)
        {
            MainWindow.notifier.ClearMessages(new ClearAll());
            mainWindow.Main.Navigate(new HomePage());
        }

        private void DeleteListing(object sender, RoutedEventArgs e)
        {
            string id = ((Button)sender).Tag as string;

            var deleteFilter = Builders<House>.Filter.Eq("_id", new ObjectId(id));
            MainWindow.houseCollection.DeleteOne(deleteFilter);
            House houseToRemove = AllHouses.Where(i => i.Id == id).Single();
            AllHouses.Remove(houseToRemove);
            houses.Remove(houseToRemove);
            MainWindow.notifier.ShowSuccess("Listing successfully deleted");
        }

        private bool ListingFilter(object item)
        {
            if (String.IsNullOrWhiteSpace(ListingsSearch.Text))
            {
                return true;
            }
            else
            {
                House house = item as House;
                return house.Address.IndexOf(ListingsSearch.Text, StringComparison.OrdinalIgnoreCase) >= 0
                    || house.City.IndexOf(ListingsSearch.Text, StringComparison.OrdinalIgnoreCase) >= 0
                    || house.Province.IndexOf(ListingsSearch.Text, StringComparison.OrdinalIgnoreCase) >= 0;
            }
        }

        private void ListingSearch(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(MyControl.ItemsSource).Refresh();
        }

        private void SoldClick(object sender, RoutedEventArgs e)
        {
            SoldButton.Background = Brushes.White;

            Panel.SetZIndex(SoldButton, 10);
            Panel.SetZIndex(ActiveButton, 0);
            houses = new ObservableCollection<House>(AllHouses.Skip(AllHouses.Count - 1));
            MyControl.ItemsSource = houses;

            var bc = new BrushConverter();
            ActiveButton.Background = bc.ConvertFrom("#FF9B9797") as Brush;
        }

        private void HouseEdit(object sender, RoutedEventArgs e)
        {
            string id = ((Button)sender).Tag as string;
            MainWindow.notifier.ClearMessages(new ClearAll());
            mainWindow.Main.Navigate(new AddListing(houses.Where(i => i.Id == id).Single()));
        }

        private void ActiveClick(object sender, RoutedEventArgs e)
        {
            ActiveButton.Background = Brushes.White;

            Panel.SetZIndex(ActiveButton, 10);
            Panel.SetZIndex(SoldButton, 0);
            houses = new ObservableCollection<House>(AllHouses.Take(AllHouses.Count - 1));
            MyControl.ItemsSource = houses;

            var bc = new BrushConverter();
            SoldButton.Background = bc.ConvertFrom("#FF9B9797") as Brush;
        }

        private void RealtorLoaded(object sender, RoutedEventArgs e)
        {
            MyControl.ItemsSource = houses;
            if (justPosted)
            {
                MainWindow.notifier.ShowSuccess("Posting created successfully");
            }
            else if (justUpdated)
            {
                MainWindow.notifier.ShowSuccess("Posting updated successfully");
            }
        }

        private void HomeClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            HomeButtonClick(null, null);
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