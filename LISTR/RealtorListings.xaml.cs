using System;
using System.Collections.Generic;
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
        public static ObservableCollection<House> houses = new ObservableCollection<House>(MainWindow.houses.AsQueryable().ToList());
        private readonly MainWindow mainWindow;

        public RealtorListings()
        {
            mainWindow = (MainWindow)Application.Current.MainWindow;
            Console.WriteLine("Num Houses: " + houses.Count);
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

        private void RealtorLoaded(object sender, RoutedEventArgs e)
        {
            MyControl.ItemsSource = houses;
        }
    }
}