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
    }
}
