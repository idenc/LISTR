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

namespace LISTR
{
    /// <summary>
    /// Interaction logic for Filters.xaml
    /// </summary>
    /// <Grid.Background>
    public partial class Filters : Page
    {
        public Filters()
        {
            InitializeComponent();
            RentSwitch.Background = Brushes.LightGray;
            BuySwitch.Background = Brushes.Beige;
            Tag1.Background = Brushes.Gray;
            Tag2.Background = Brushes.Gray;
            Tag3.Background = Brushes.Gray;
            Tag4.Background = Brushes.Gray;
            Tag5.Background = Brushes.Gray;
            Tag6.Background = Brushes.Gray;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Buy_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            btn.Background = Brushes.Beige;
            RentSwitch.Background = Brushes.LightGray;
        }
        private void Rent_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            btn.Background = Brushes.Beige;
            BuySwitch.Background = Brushes.LightGray;
        }
        private void Tag_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            if (btn.Background == Brushes.LightCyan) { btn.Background = Brushes.Gray; }
            else { btn.Background = Brushes.LightCyan; }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ListingSearch(object sender, TextChangedEventArgs e)
        {

        }
    }
}
