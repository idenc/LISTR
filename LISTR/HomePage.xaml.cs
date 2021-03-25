using System;
using System.Windows;
using System.Windows.Controls;
using MongoDB.Bson;
using MongoDB.Driver;

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
            if (string.IsNullOrWhiteSpace(HomepageUsername.Text) || string.IsNullOrWhiteSpace(HomepagePassword.Password))
            {
                // This shouldn't happen since login button is not enabled until boxes are not empty
                // but check anyways
                return;
            }

            var filter = Builders<BsonDocument>.Filter.Eq("username", HomepageUsername.Text);
            var search = MainWindow.accounts.Find(filter).FirstOrDefault();
            if (search != null)
            {
                String password = search["password"].AsString;
                if (password == HomepagePassword.Password)
                {
                    LoginPanel.Visibility = Visibility.Collapsed;
                    RegisterButton.Visibility = Visibility.Collapsed;
                    LogoutButton.Visibility = Visibility.Visible;
                    return;
                }
            }

            LoginError.Text = "Username or password is incorrect";
            LoginPopup.IsOpen = true;
        }

        private void OpenRegister(object sender, RoutedEventArgs e)
        {
            new Register
            {
                Placement = System.Windows.Controls.Primitives.PlacementMode.Center,
                PlacementTarget = this,
                IsOpen = true,
                AllowsTransparency = true
            };
        }

        private void LoginChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(HomepageUsername.Text) && !string.IsNullOrWhiteSpace(HomepagePassword.Password))
            {
                LoginButton.IsEnabled = true;
            }
            else
            {
                LoginButton.IsEnabled = false;
            }
        }
    }
}