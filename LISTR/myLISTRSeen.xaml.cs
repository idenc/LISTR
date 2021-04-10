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
using MongoDB.Driver;
using System.Collections.ObjectModel;
using System.Globalization;

namespace LISTR
{
    /// <summary>
    /// Interaction logic for myLISTRSeen.xaml
    /// </summary>
    public partial class myLISTRSeen : Page
    {
        private Page _previousPage;
        public static ObservableCollection<House> skipped = new ObservableCollection<House>();

        public myLISTRSeen(Page previousPage)
        {
            this._previousPage = previousPage;
            InitializeComponent();
        }

        private void resetClick(object sender, RoutedEventArgs e)
        {
            skipped.Clear();
        }

        private void ContactRealtorClick(object sender, RoutedEventArgs e)
        {
            new ContactRealtor(this)
            {
                Placement = System.Windows.Controls.Primitives.PlacementMode.Center,
                PlacementTarget = this,
                IsOpen = true
            };
        }

        private void backClick(object sender, RoutedEventArgs e)
        {
            var MainWindow = (MainWindow)Application.Current.MainWindow;
            MainWindow.Main.Navigate(_previousPage);
        }

        private void HomeClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var MainWindow = (MainWindow)Application.Current.MainWindow;
            MainWindow.Main.Navigate(new HomePage());
        }

        private void myListrFavourites(object sender, RoutedEventArgs e)
        {
            var MainWindow = (MainWindow)Application.Current.MainWindow;
            MainWindow.Main.Navigate(new myLISTR(_previousPage));
        }

        private void myListrDisliked(object sender, RoutedEventArgs e)
        {
            var MainWindow = (MainWindow)Application.Current.MainWindow;
            MainWindow.Main.Navigate(new myLISTRDislike(_previousPage));
        }

        private void moveListingFavourite(object sender, RoutedEventArgs e)
        {
            string id = ((Button)sender).Tag as string;
            myLISTR.favourites.Add(skipped.Where(i => i.Id == id).Single());
            skipped.Remove(skipped.Where(i => i.Id == id).Single());
        }

        private void moveListingDisliked(object sender, RoutedEventArgs e)
        {
            string id = ((Button)sender).Tag as string;
            myLISTRDislike.disliked.Add(skipped.Where(i => i.Id == id).Single());
            skipped.Remove(skipped.Where(i => i.Id == id).Single());
        }


    }


}
