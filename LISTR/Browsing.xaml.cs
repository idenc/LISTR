using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
        private myLISTR myLISTRPage;
        private bool isPreview = false;
        private bool isViewDetails = false;
        private Browsecontrol browsecontrol;

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
            isPreview = true;

            InitializeComponent();
        }

        public Browsing(House house, myLISTR myLISTRPage)
        {
            mainWindow = (MainWindow)Application.Current.MainWindow;
            this.myLISTRPage = myLISTRPage;
            houses = new List<House>
            {
                house
            };
            isViewDetails = true;

            InitializeComponent();
        }

        public Browsing()
        {
            houses = new List<House>
            {
                SampleHouse
            };
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
            mainWindow.Main.Navigate(new RealtorListings(true, false));
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

        private void HandleKeyPress(object sender, KeyEventArgs e)
        {
            if (index < houses.Count)
            {
                if (e.Key == Key.Left)
                {
                    myLISTR.favourites.Add(houses[index++]);
                    browsecontrol.FavouriteButton.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                    e.Handled = true;
                }
                else if (e.Key == Key.Right)
                {
                    myLISTR.disliked.Add(houses[index++]);
                    browsecontrol.DislikeButton.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                    e.Handled = true;
                }
                else if (e.Key == Key.Up || e.Key == Key.Down)
                {
                    myLISTR.skipped.Add(houses[index++]);
                    browsecontrol.SkipButton.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
                    e.Handled = true;
                }
            }
        }

        public void AdvanceHouse()
        {
            if (index < houses.Count)
            {
                browsecontrol = new Browsecontrol(houses[index++], this);
                Transitor.SlideLeft(browsecontrol);
            }
        }

        private void BackToMyListr(object sender, RoutedEventArgs e)
        {
            if (myLISTRPage != null)
            {
                mainWindow.Main.Navigate(myLISTRPage);
            }
        }

        private void BrowsingLoaded(object sender, RoutedEventArgs e)
        {
            SearchBox.Watermark = search;
            Focus();
            if (index < houses.Count)
            {
                browsecontrol = new Browsecontrol(houses[index++], this);
                Transitor.currentPresenter.Content = browsecontrol;

                if (isPreview)
                {
                    TextBlock tb = new TextBlock
                    {
                        TextAlignment = TextAlignment.Center,
                        Text = "Cancel\nPreview"
                    };
                    ViewFavouritesButton.Content = tb;

                    PostButton.Visibility = Visibility.Visible;

                    ViewFavouritesButton.Click -= moveToFavourites;

                    ViewFavouritesButton.Click += GoToPreviousPage;

                    SearchBar.Visibility = Visibility.Collapsed;
                }
                else if (isViewDetails)
                {
                    TextBlock tb = new TextBlock
                    {
                        TextAlignment = TextAlignment.Center,
                        Text = "Back to\nMy LISTR"
                    };
                    ViewFavouritesButton.Content = tb;
                    ViewFavouritesButton.Click -= moveToFavourites;
                    ViewFavouritesButton.Click += BackToMyListr;
                    browsecontrol.SkipButton.Visibility = Visibility.Collapsed;
                    browsecontrol.FavouriteButton.Visibility = Visibility.Collapsed;
                    browsecontrol.DislikeButton.Visibility = Visibility.Collapsed;
                }
            }
            else if (Transitor.currentPresenter.Content == null)
            {
                TextBlock tb = new TextBlock
                {
                    Text = "No listings found for search " + search,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                };
                Transitor.TransitorGrid.Children.Add(tb);
            }
        }

        private void SearchClick(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(SearchBox.Text))
            {
                return;
            }

            HomePage.DoHouseSearch(SearchBox.Text);
        }
    }
}