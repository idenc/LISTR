using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MongoDB.Driver;

namespace LISTR

{
    /// <summary>
    /// Interaction logic for myLISTR.xaml
    /// </summary>
    public partial class myLISTR : Page
    {
        private Page _previousPage;
        public static ObservableCollection<House> favourites = new ObservableCollection<House>(MainWindow.favouritedHouse.AsQueryable().ToList());

        public myLISTR(Page previousPage)
        {
            this._previousPage = previousPage;
            InitializeComponent();
        }

        private void backClick(object sender, RoutedEventArgs e)
        {
            var MainWindow = (MainWindow)Application.Current.MainWindow;
            MainWindow.Main.Navigate(_previousPage);
        }

        private void resetClick(object sender, RoutedEventArgs e)
        {
            MainWindow.favouritedHouse.Clear();
            favourites.Clear();
        }
    }
}