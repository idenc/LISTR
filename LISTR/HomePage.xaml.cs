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

        public void DoLogin(Boolean isRealtor, string username)
        {
            LoginPanel.Visibility = Visibility.Collapsed;
            RegisterButton.Visibility = Visibility.Collapsed;
            LogoutButton.Visibility = Visibility.Visible;
            if (isRealtor)
            {
                ListingsButton.Visibility = Visibility.Visible;
            }
            // Save login info as global variables
            Application.Current.Properties["Username"] = HomepageUsername.Text;
            Application.Current.Properties["IsRealtor"] = isRealtor;
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
                // User found
                String password = search["password"].AsString;
                if (password == HomepagePassword.Password)
                {
                    // Password is correct
                    DoLogin(search["is_realtor"].AsBoolean, HomepageUsername.Text);
                    return;
                }
            }

            // Show error message
            LoginError.Text = "Username or password is incorrect";
            LoginPopup.IsOpen = true;
        }

        private void LogoutClick(object sender, RoutedEventArgs e)
        {
            // Reset buttons
            LogoutButton.Visibility = Visibility.Collapsed;
            ListingsButton.Visibility = Visibility.Collapsed;

            LoginPanel.Visibility = Visibility.Visible;
            RegisterButton.Visibility = Visibility.Visible;
        }

        private void OpenRegister(object sender, RoutedEventArgs e)
        {
            new Register(this)
            {
                Placement = System.Windows.Controls.Primitives.PlacementMode.Center,
                PlacementTarget = this,
                IsOpen = true
            };
        }

        private void LoginChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(HomepageUsername.Text) && !string.IsNullOrWhiteSpace(HomepagePassword.Password))
            {
                LoginButton.IsEnabled = true;
                SearchButton.IsDefault = false;
                LoginButton.IsDefault = true;
            }
            else
            {
                LoginButton.IsEnabled = false;
            }
        }

        private void ListingsClick(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.Main.Navigate(new RealtorListings());
        }

        private void SearchClick(object sender, RoutedEventArgs e)
        {
            string search = SearchBar.Text.ToLower();
            if (String.IsNullOrWhiteSpace(search))
            {
                return;
            }

            var result = MainWindow.houses.FindAll(x => x.Address.ToLower().Contains(search) || x.City.ToLower().Contains(search) || x.Province.ToLower().Contains(search));

            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.Main.Navigate(new Browsing(result, search));
        }

        private void HomePageLoaded(object sender, RoutedEventArgs e)
        {
            if (Application.Current.Properties.Contains("Username"))
            {
                bool is_realtor = false;
                if (Application.Current.Properties.Contains("IsRealtor"))
                {
                    is_realtor = (bool)Application.Current.Properties["IsRealtor"];
                }
                // The user is logged in
                DoLogin(is_realtor, (string)Application.Current.Properties["Username"]);
            }
        }

        private void SearchBarClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            SearchButton.IsDefault = true;
            LoginButton.IsDefault = false;
        }
    }
}