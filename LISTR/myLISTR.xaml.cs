using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Globalization;
using MongoDB.Driver;

namespace LISTR


{
    /// <summary>
    /// Interaction logic for myLISTR.xaml
    /// </summary>
    public partial class myLISTR : Page
    {
        public static ObservableCollection<House> houses = new ObservableCollection<House>(MainWindow.houses.AsQueryable().ToList());
        private readonly MainWindow mainWindow;


        public myLISTR()
        {
            mainWindow = (MainWindow)Application.Current.MainWindow;
            Console.WriteLine("Num Houses: " + houses.Count);
            InitializeComponent();

            //Hard code filters here
            string id = "60637c13c9c52a0dc8e0e3a7";
            houses.Remove(houses.Where(i => i.Id == id).Single());
            id = "606ceb8e6150cae9ba48a1cd";
            houses.Remove(houses.Where(i => i.Id == id).Single());


            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(MyControl.ItemsSource);
            view.Filter = ListingFilter;
        }

        private bool ListingFilter(object item)
        {
            if (String.IsNullOrWhiteSpace(ListingsSearch.Text))
                return true;
            else
                return ((item as House).Address.IndexOf(ListingsSearch.Text, StringComparison.OrdinalIgnoreCase) >= 0);
        }

        public void OpenFilters()
        {
            
        }

        private void OpenFilters(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.Main.Navigate(new Filters());
        }
    }
}