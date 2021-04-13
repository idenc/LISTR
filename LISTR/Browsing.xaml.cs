using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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

        private void PreviewPost(object sender, RoutedEventArgs e)
        {
            addListingPage.PostListing(null, null);
            mainWindow.Main.Navigate(new RealtorListings());
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

        private void RightKeyPress(object sender, KeyEventArgs e)
        {
            Console.WriteLine("Keypress");
            if (e.Key == Key.Right)
            {
                if (index < houses.Count)
                {
                    AdvanceHouse();
                }
            }
        }

        public void AdvanceHouse()
        {
            if (index < houses.Count)
            {
                var browseControl = new Browsecontrol(houses[index++], this);
                Transitor.SlideLeft(browseControl);
            }
        }

        private void BrowsingLoaded(object sender, RoutedEventArgs e)
        {
            SearchBox.Watermark = search;
            Focus();
            if (index < houses.Count)
            {
                var browseControl = new Browsecontrol(houses[index++], this);
                Transitor.currentPresenter.Content = browseControl;
            }
            else
            {
                TextBlock tb = new TextBlock
                {
                    Text = "No listings found for search " + search,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                Transitor.TransitorGrid.Children.Add(tb);
            }

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

                //DislikeButton.PreviewMouseDown -= DislikeClick;
                //SkipButton.PreviewMouseDown -= SeenClick;
                //FavouriteButton.PreviewMouseDown -= FavouriteClick;

                //ViewDislikedButton.Visibility = Visibility.Collapsed;
                SearchBar.Visibility = Visibility.Collapsed;
            }
        }
    }
}