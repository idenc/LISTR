using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using MongoDB.Bson;
using MongoDB.Driver;

namespace LISTR
{
    /// <summary>
    /// Interaction logic for Register.xaml
    /// </summary>
    public partial class LISTRpop : Page
    {
        private readonly LISTRpop homePage;

        public LISTRpop(House listing)
        {
            DataContext = listing;
            InitializeComponent();
            
        }

        private void ImageClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Image newimage = new Image();
            newimage.Source = ((Image)sender).Source;
            dynamicImage.Source = newimage.Source;
        }

        private void ExitClick(object sender, System.Windows.RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.Main.Navigate(new myLISTR(this, myLISTR.SelectedTab.Favourites));
        }
    }
}