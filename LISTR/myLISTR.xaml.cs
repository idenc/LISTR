using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace LISTR
{
    public class MoveBinding : INotifyPropertyChanged
    {
        private string _move1 = "Hello";
        private string _move2 = "Goodbye";

        public event PropertyChangedEventHandler PropertyChanged;

        public string Move1
        {
            get => _move1;
            set
            {
                _move1 = value;
                OnPropertyChanged();
            }
        }

        public string Move2
        {
            get => _move2;
            set
            {
                _move2 = value;
                OnPropertyChanged();
            }
        }

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }

    /// <summary>
    /// Interaction logic for myLISTRSeen.xaml
    /// </summary>
    public partial class myLISTR : Page
    {
        private Page _previousPage;
        public static ObservableCollection<House> favourites = new ObservableCollection<House>();
        public static ObservableCollection<House> skipped = new ObservableCollection<House>();
        public static ObservableCollection<House> disliked = new ObservableCollection<House>();
        public MoveBinding moveBinding = new MoveBinding();

        private SelectedTab selectedTab;

        public enum SelectedTab
        {
            Favourites,
            Skipped,
            Disliked
        }

        public myLISTR(Page previousPage, SelectedTab selectedTab)
        {
            _previousPage = previousPage;
            this.selectedTab = selectedTab;

            InitializeComponent();
        }

        public myLISTR()
        {
            InitializeComponent();
        }

        private void resetClick(object sender, RoutedEventArgs e)
        {
            favourites.Clear();
        }

        private void ContactRealtorClick(object sender, RoutedEventArgs e)
        {
            new ContactRealtor()
            {
                Placement = System.Windows.Controls.Primitives.PlacementMode.Center,
                PlacementTarget = this,
                IsOpen = true
            };
        }

        private void backClick(object sender, RoutedEventArgs e)
        {
            var MainWindow = (MainWindow)Application.Current.MainWindow;
            MainWindow.Main.Navigate(_previousPage);
        }

        private void HomeClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var MainWindow = (MainWindow)Application.Current.MainWindow;
            MainWindow.Main.Navigate(new HomePage());
        }

        private void UpdateMoveButtons()
        {
            if (selectedTab == SelectedTab.Favourites)
            {
                moveBinding.Move1 = "Move to Skipped";
                moveBinding.Move2 = "Move to Disliked";
            }
            else if (selectedTab == SelectedTab.Disliked)
            {
                moveBinding.Move1 = "Move to Skipped";
                moveBinding.Move2 = "Move to Favourites";
            }
            else
            {
                moveBinding.Move1 = "Move to Favourites";
                moveBinding.Move2 = "Move to Disliked";
            }
        }

        private void FavouritesClick(object sender, RoutedEventArgs e)
        {
            selectedTab = SelectedTab.Favourites;
            FavouritesButton.Background = Brushes.White;

            Panel.SetZIndex(FavouritesButton, 2);
            Panel.SetZIndex(SkippedButton, 1);
            Panel.SetZIndex(DislikedButton, 0);

            var bc = new BrushConverter();
            DislikedButton.Background = bc.ConvertFrom("#FF9B9797") as Brush;
            SkippedButton.Background = bc.ConvertFrom("#FF9B9797") as Brush;
            MyControl.ItemsSource = favourites;
            UpdateMoveButtons();
        }

        private void DislikedClick(object sender, RoutedEventArgs e)
        {
            selectedTab = SelectedTab.Disliked;
            DislikedButton.Background = Brushes.White;

            Panel.SetZIndex(DislikedButton, 2);
            Panel.SetZIndex(SkippedButton, 1);
            Panel.SetZIndex(DislikedButton, 0);

            var bc = new BrushConverter();
            FavouritesButton.Background = bc.ConvertFrom("#FF9B9797") as Brush;
            SkippedButton.Background = bc.ConvertFrom("#FF9B9797") as Brush;
            MyControl.ItemsSource = disliked;
        }

        private void SkippedClick(object sender, RoutedEventArgs e)
        {
            selectedTab = SelectedTab.Skipped;
            SkippedButton.Background = Brushes.White;

            Panel.SetZIndex(FavouritesButton, 1);
            Panel.SetZIndex(SkippedButton, 2);
            Panel.SetZIndex(DislikedButton, 0);

            var bc = new BrushConverter();
            FavouritesButton.Background = bc.ConvertFrom("#FF9B9797") as Brush;
            DislikedButton.Background = bc.ConvertFrom("#FF9B9797") as Brush;
            MyControl.ItemsSource = skipped;
        }

        private void MyLISTRLoaded(object sender, RoutedEventArgs e)
        {
            if (selectedTab == SelectedTab.Favourites)
            {
                FavouritesClick(null, null);
            }
            else if (selectedTab == SelectedTab.Disliked)
            {
                DislikedClick(null, null);
            }
            else
            {
                SkippedClick(null, null);
            }
        }

        private void moveListing(object sender, ObservableCollection<House> destList)
        {
            string id = ((Button)sender).Tag as string;

            if (selectedTab == SelectedTab.Favourites)
            {
                House listing = favourites.Where(i => i.Id == id).Single();
                favourites.Remove(listing);
                destList.Add(listing);
            }
            else if (selectedTab == SelectedTab.Skipped)
            {
                House listing = skipped.Where(i => i.Id == id).Single();
                skipped.Remove(listing);
                destList.Add(listing);
            }
            else
            {
                House listing = disliked.Where(i => i.Id == id).Single();
                disliked.Remove(listing);
                destList.Add(listing);
            }
        }

        private void moveListingFavourite(object sender, RoutedEventArgs e)
        {
            moveListing(sender, favourites);
        }

        private void moveListingDisliked(object sender, RoutedEventArgs e)
        {
            moveListing(sender, disliked);
        }

        private void moveListingSkipped(object sender, RoutedEventArgs e)
        {
            moveListing(sender, skipped);
        }
    }
}