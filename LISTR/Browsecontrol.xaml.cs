using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace LISTR
{
    /// <summary>
    /// Interaction logic for Browsecontrol.xaml
    /// </summary>
    public partial class Browsecontrol : UserControl
    {
        private House house;
        private Browsing browsingPage;
        private bool isPreview = false;

        public Browsecontrol(House house, Browsing browsingPage, bool isPreview = false)
        {
            DataContext = house;
            this.house = house;
            this.browsingPage = browsingPage;
            this.isPreview = isPreview;
            InitializeComponent();
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

        private void FavouriteClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!myLISTR.favourites.Any(x => x.Id == house.Id))
            {
                myLISTR.favourites.Add(house);
            }
            browsingPage.AdvanceHouse();
        }

        private void SkipClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!myLISTR.skipped.Any(x => x.Id == house.Id))
            {
                myLISTR.skipped.Add(house);
            }
            browsingPage.AdvanceHouse();
        }

        private void DislikeClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (!myLISTR.disliked.Any(x => x.Id == house.Id))
            {
                myLISTR.disliked.Add(house);
            }
            browsingPage.AdvanceHouse();
        }

        private void BrowsecontrolLoaded(object sender, RoutedEventArgs e)
        {
            if (isPreview)
            {
                DislikeButton.PreviewMouseDown -= DislikeClick;
                SkipButton.PreviewMouseDown -= SkipClick;
                FavouriteButton.PreviewMouseDown -= FavouriteClick;
            }
        }
    }
}