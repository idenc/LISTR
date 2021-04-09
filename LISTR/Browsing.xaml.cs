using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using MongoDB.Driver;

namespace LISTR
{
    /// <summary>
    /// Interaction logic for Browsing.xaml
    /// </summary>
    public partial class Browsing : Page
    {
        private MainWindow mainWindow;
        public static House SampleHouse = MainWindow.houses.AsQueryable().ToList()[0];
        private string search;
        private List<House> houses;
        private int index = 0;

        public Browsing(List<House> houses, string search)
        {
            mainWindow = (MainWindow)Application.Current.MainWindow;
            try
            {
                this.search = search;
                this.houses = houses;
                DataContext = houses[index];
            }
            catch (Exception ex)
            {
                if (ex is NullReferenceException || ex is ArgumentOutOfRangeException)
                {
                    DataContext = SampleHouse;
                }
                else
                {
                    throw;
                }
            }

            InitializeComponent();
        }

        public Browsing()
        {
            DataContext = SampleHouse;
            InitializeComponent();
        }

        private void FavouriteClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {   

                MainWindow.favouritedHouse.Add(houses[index]);
            

            if (index < houses.Count - 1)
            {
                DataContext = houses[++index];
                Console.WriteLine("Hi");
            }


        }

        private void BrowsingLoaded(object sender, RoutedEventArgs e)
        {
            SearchBox.Watermark = this.search;
        }

        private void ListrFavourite(object sender, RoutedEventArgs e)
        {
            var MainWindow = (MainWindow)Application.Current.MainWindow;
            MainWindow.Main.Navigate(new myLISTR(this));
            MainWindow.fromBrowse = true;
        }
    }
}