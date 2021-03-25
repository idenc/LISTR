using System;
using System.Collections.Generic;
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
        public List<House> houses = MainWindow.houses.AsQueryable().ToList();
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