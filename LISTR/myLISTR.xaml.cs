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
    public partial class myLISTR : Page
    {
        private Page _previousPage;
        public static ObservableCollection<House> favourites = new ObservableCollection<House>();

        public myLISTR(Page previousPage)
        {
            this._previousPage = previousPage;
            InitializeComponent();
        }

        private void resetClick(object sender, RoutedEventArgs e)
        {
            favourites.Clear();
        }

        private void ContactRealtorClick(object sender, RoutedEventArgs e)
        {
            new ContactRealtor()
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

        private void myListrSkipped(object sender, RoutedEventArgs e)
        {
            var MainWindow = (MainWindow)Application.Current.MainWindow;
        }

        private void myListrDisliked(object sender, RoutedEventArgs e)
        {
            var MainWindow = (MainWindow)Application.Current.MainWindow;
        }

        private void moveListingSkipped(object sender, RoutedEventArgs e)
        {
            //string id = ((Button)sender).Tag as string;
            //myLISTRSeen.skipped.Add(favourites.Where(i => i.Id == id).Single());
            //favourites.Remove(favourites.Where(i => i.Id == id).Single());
        }

        private void moveListingDisliked(object sender, RoutedEventArgs e)
        {
            //string id = ((Button)sender).Tag as string;
            //myLISTRDislike.disliked.Add(favourites.Where(i => i.Id == id).Single());
            //favourites.Remove(favourites.Where(i => i.Id == id).Single());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }


}
