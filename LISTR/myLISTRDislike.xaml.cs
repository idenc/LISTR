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
    /// Interaction logic for myLISTRDislike.xaml
    /// </summary>
    public partial class myLISTRDislike : Page
    {
        private Page _previousPage;
        public static ObservableCollection<House> disliked = new ObservableCollection<House>();

        public myLISTRDislike(Page previousPage)
        {
            this._previousPage = previousPage;
            InitializeComponent();
        }

        private void moveToFavourites(object sender, RoutedEventArgs e)
        {
            var MainWindow = (MainWindow)Application.Current.MainWindow;
            MainWindow.Main.Navigate(new myLISTR(_previousPage));
        }

        public void moveToSeen(object sender, RoutedEventArgs e)
        {
            var MainWindow = (MainWindow)Application.Current.MainWindow;
            MainWindow.Main.Navigate(new myLISTRSeen(_previousPage));
        }

        private void backClick(object sender, RoutedEventArgs e)
        {
            var MainWindow = (MainWindow)Application.Current.MainWindow;
            MainWindow.Main.Navigate(_previousPage);
        }

        private void resetClick(object sender, RoutedEventArgs e)
        {
            disliked.Clear();
        }

        private void moveListingSeen(object sender, RoutedEventArgs e)
        {
            string id = ((Button)sender).Tag as string;
            myLISTRSeen.skipped.Add(disliked.Where(i => i.Id == id).Single());
            disliked.Remove(disliked.Where(i => i.Id == id).Single());
        }

        private void moveListingFavourite(object sender, RoutedEventArgs e)
        {
            string id = ((Button)sender).Tag as string;
            myLISTR.favourites.Add(disliked.Where(i => i.Id == id).Single());
            disliked.Remove(disliked.Where(i => i.Id == id).Single());
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
    }
}
