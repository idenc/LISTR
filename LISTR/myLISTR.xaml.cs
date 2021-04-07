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
    /// Interaction logic for myLISTR.xaml
    /// </summary>
    public partial class myLISTR : Page
    {
        private MainWindow mainWindow;
        public static ObservableCollection<House> favourites = new ObservableCollection<House>(MainWindow.favouritedHouse.AsQueryable().ToList());


        public myLISTR()
        {
            InitializeComponent();
        }

        private void backClick(object sender, RoutedEventArgs e)
        {
            if(MainWindow.fromBrowse)
            {
                var MainWindow = (MainWindow)Application.Current.MainWindow;
                MainWindow.Main.Navigate(new Browsing());
                MainWindow.fromBrowse = false;
            }
            else if(!MainWindow.fromBrowse)
            {
                var MainWindow = (MainWindow)Application.Current.MainWindow;
                MainWindow.Main.Navigate(new HomePage());
                MainWindow.fromBrowse = false;
            }
        }

        private void resetClick(object sender, RoutedEventArgs e)
        {
            MainWindow.favouritedHouse.Clear();
            favourites.Clear();
        }
    }
}