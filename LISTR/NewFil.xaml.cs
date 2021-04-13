using System.Windows.Controls.Primitives;
using System.Windows.Controls;
using System.Windows.Media;

using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Windows;


namespace LISTR
{
    /// <summary>
    /// Interaction logic for Filters1.xaml
    /// </summary>
    public partial class NewFil : Popup
    {

        private readonly myLISTR Listr;

        public NewFil()
        {
            InitializeComponent();
            RentSwitch.Background = Brushes.LightGray;
            BuySwitch.Background = Brushes.Beige;
        }

        private void Confirm(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void Cancel(object sender, System.Windows.RoutedEventArgs e)
        {
               var mainWindow = (MainWindow)Application.Current.MainWindow;
               mainWindow.Main.Navigate(new myLISTR(Listr, myLISTR.SelectedTab.Favourites));
        }

        private void Buy(object sender, System.Windows.RoutedEventArgs e)
        {
            Button btn = sender as Button;
            btn.Background = Brushes.Beige;
            RentSwitch.Background = Brushes.LightGray;
        }

        private void Rent(object sender, System.Windows.RoutedEventArgs e)
        {
            Button btn = sender as Button;
            btn.Background = Brushes.Beige;
            BuySwitch.Background = Brushes.LightGray;
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void TagClick(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn.Background == Brushes.LightCyan) { btn.Background = Brushes.Gray; }
            else { btn.Background = Brushes.LightCyan; }
        }
    }
}