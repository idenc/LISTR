using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MongoDB.Driver;

namespace LISTR
{
    /// <summary>
    /// Interaction logic for Browsing.xaml
    /// </summary>
    public partial class Browsing : Page
    {
        private MainWindow mainWindow;
        public static House SampleHouse = MainWindow.houses.AsQueryable().ToList()[0];
        private string search;
        private List<House> houses;
        private int index = 0;
        private AddListing addListingPage;
        private bool isPreview = false;

        public Browsing(List<House> houses, string search, bool isPreview = false)
        {
            mainWindow = (MainWindow)Application.Current.MainWindow;
            try
            {
                this.search = search;
                this.houses = houses;
                DataContext = houses[index];
            }
            catch (Exception ex)
            {
                if (ex is NullReferenceException || ex is ArgumentOutOfRangeException)
                {
                    DataContext = SampleHouse;
                }
                else
                {
                    throw;
                }
            }

            InitializeComponent();
        }

        public Browsing(House house, AddListing addListingPage)
        {
            mainWindow = (MainWindow)Application.Current.MainWindow;
            this.addListingPage = addListingPage;
            houses = new List<House>
            {
                house
            };
            DataContext = house;
            isPreview = true;

            InitializeComponent();
        }

        public Browsing()
        {
            DataContext = SampleHouse;
            InitializeComponent();
        }

        private void GoToPreviousPage(object sender, RoutedEventArgs e)
        {
            if (addListingPage != null)
            {
                mainWindow.Main.Navigate(addListingPage);
            }
        }

        private void FavouriteClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (index < houses.Count - 1)
            {
                myLISTR.favourites.Add(houses[index]);
                DataContext = houses[++index];
            }
        }

        private void SeenClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (index < houses.Count - 1)
            {
                myLISTR.skipped.Add(houses[index]);
                DataContext = houses[++index];
            }
        }

        private void DislikeClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (index < houses.Count - 1)
            {
                myLISTR.disliked.Add(houses[index]);
                DataContext = houses[++index];
            }
        }

        private void BrowsingLoaded(object sender, RoutedEventArgs e)
        {
            SearchBox.Watermark = this.search;
            if (isPreview)
            {
                TextBlock tb = new TextBlock
                {
                    TextAlignment = TextAlignment.Center,
                    Text = "Cancel\nPreview"
                };
                ViewFavouritesButton.Content = tb;

                ViewSkippedButton.Content = "Post";

                ViewFavouritesButton.Click -= moveToFavourites;
                ViewSkippedButton.Click -= moveToSkipped;

                ViewFavouritesButton.Click += GoToPreviousPage;
                ViewSkippedButton.Click += PreviewPost;

                DislikeButton.PreviewMouseDown -= DislikeClick;
                SkipButton.PreviewMouseDown -= SeenClick;
                FavouriteButton.PreviewMouseDown -= FavouriteClick;

                ViewDislikedButton.Visibility = Visibility.Collapsed;
                SearchBar.Visibility = Visibility.Collapsed;
            }
        }

        private void PreviewPost(object sender, RoutedEventArgs e)
        {
            addListingPage.PostListing(null, null);
            mainWindow.Main.Navigate(new RealtorListings());
        }

        private void ImageClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Image newimage = new Image();
            newimage.Source = ((Image)sender).Source;
            dynamicImage.Source = newimage.Source;
        }

        private void ContactRealtorClick(object sender, RoutedEventArgs e)
        {
            new ContactRealtor(this)
            {
                Placement = System.Windows.Controls.Primitives.PlacementMode.Center,
                PlacementTarget = this,
                IsOpen = true
            };
        }

        private void HomeClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            mainWindow.Main.Navigate(new HomePage());
        }

        private void moveToFavourites(object sender, RoutedEventArgs e)
        {
            var MainWindow = (MainWindow)Application.Current.MainWindow;
            MainWindow.Main.Navigate(new myLISTR(this, myLISTR.SelectedTab.Favourites));
        }

        private void moveToSkipped(object sender, RoutedEventArgs e)
        {
            var MainWindow = (MainWindow)Application.Current.MainWindow;
            MainWindow.Main.Navigate(new myLISTR(this, myLISTR.SelectedTab.Skipped));
        }

        private void moveToDisliked(object sender, RoutedEventArgs e)
        {
            var MainWindow = (MainWindow)Application.Current.MainWindow;
            MainWindow.Main.Navigate(new myLISTR(this, myLISTR.SelectedTab.Disliked));
        }
    }
}