using System;
using System.Windows;
using System.Windows.Controls;

namespace LISTR
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void MediaEnded(object sender, RoutedEventArgs e)
        {
            backgroundVideo.Position = TimeSpan.FromMilliseconds(1);
        }

        private void LoginClick(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = (MainWindow)Window.GetWindow(this);
            Login login = new Login();
            mainWindow.Main.Navigate(login);
        }
    }
}
