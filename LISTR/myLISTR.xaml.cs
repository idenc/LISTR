using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;

namespace LISTR
{
    public class MoveBinding : INotifyPropertyChanged
    {
        private string _move1 = "Move to Skipped";
        private string _move2 = "Move to Disliked";

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
        public static MoveBinding moveBinding = new MoveBinding();

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

        private void ResetClick(object sender, RoutedEventArgs e)
        {
            if (selectedTab == SelectedTab.Favourites)
            {
                favourites.Clear();
            }
            else if (selectedTab == SelectedTab.Disliked)
            {
                disliked.Clear();
            }
            else
            {
                skipped.Clear();
            }
        }

        private bool ListingFilter(object item)
        {
            if (String.IsNullOrWhiteSpace(ListingsSearch.Text))
            {
                return true;
            }
            else
            {
                House house = item as House;
                return house.Address.IndexOf(ListingsSearch.Text, StringComparison.OrdinalIgnoreCase) >= 0 
                    || house.City.IndexOf(ListingsSearch.Text, StringComparison.OrdinalIgnoreCase) >= 0 
                    || house.Province.IndexOf(ListingsSearch.Text, StringComparison.OrdinalIgnoreCase) >= 0;
            }
        }

        private void ListingSearch(object sender, TextChangedEventArgs e)
        {
            CollectionViewSource.GetDefaultView(MyControl.ItemsSource).Refresh();
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
            Panel.SetZIndex(FavouritesButton, 0);

            var bc = new BrushConverter();
            FavouritesButton.Background = bc.ConvertFrom("#FF9B9797") as Brush;
            SkippedButton.Background = bc.ConvertFrom("#FF9B9797") as Brush;
            MyControl.ItemsSource = disliked;
            UpdateMoveButtons();
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
            UpdateMoveButtons();
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

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(MyControl.ItemsSource);
            view.Filter = ListingFilter;
        }

        private void MoveListing(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            string id = button.Tag as string;
            string content = (button.Content as TextBlock).Text;

            ObservableCollection<House> destList;
            if (content == "Move to Favourites")
            {
                destList = favourites;
            }
            else if (content == "Move to Skipped")
            {
                destList = skipped;
            }
            else
            {
                destList = disliked;
            }

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

        private void DetailsClick(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            string id = button.Tag as string;
            Console.WriteLine(id);
            var mainWindow = (MainWindow)Application.Current.MainWindow;

            if (selectedTab == SelectedTab.Favourites)
            {
                House listing = favourites.Where(i => i.Id == id).Single();
                mainWindow.Main.Navigate(new LISTRpop(listing));
            }
            else if (selectedTab == SelectedTab.Skipped)
            {
                House listing = skipped.Where(i => i.Id == id).Single();
                mainWindow.Main.Navigate(new LISTRpop(listing));
            }
            else
            {
                House listing = disliked.Where(i => i.Id == id).Single();
                mainWindow.Main.Navigate(new LISTRpop(listing));
            }


           
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            new NewFil()
            {
                Placement = System.Windows.Controls.Primitives.PlacementMode.Center,
                PlacementTarget = this,
                IsOpen = true
            };
        }
    }
}