using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Threading;

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
        private bool hasBeenAdded = false;

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

        private void FavouriteClick(object sender, RoutedEventArgs e)
        {
            if (hasBeenAdded)
            {
                return;
            }
            if (!myLISTR.favourites.Any(x => x.Id == house.Id))
            {
                myLISTR.favourites.Add(house);
                hasBeenAdded = true;
            }
            AnimationClick(sender as Button);
        }

        private void SkipClick(object sender, RoutedEventArgs e)
        {
            if (hasBeenAdded)
            {
                return;
            }
            if (!myLISTR.skipped.Any(x => x.Id == house.Id))
            {
                myLISTR.skipped.Add(house);
                hasBeenAdded = true;
            }
            AnimationClick(sender as Button);
        }

        private void DislikeClick(object sender, RoutedEventArgs e)
        {
            if (hasBeenAdded)
            {
                return;
            }
            if (!myLISTR.disliked.Any(x => x.Id == house.Id))
            {
                myLISTR.disliked.Add(house);
                hasBeenAdded = true;
            }
            AnimationClick(sender as Button);
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

        private void HandleScroll(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            ScrollViewer scv = (ScrollViewer)sender;
            scv.ScrollToVerticalOffset(scv.VerticalOffset - e.Delta / 10);
            e.Handled = true;
        }

        private void AnimationClick(Button srcButton)
        {
            Thickness margin;
            Button animationButton;
            if (srcButton.Name == "FavouriteButton")
            {
                margin = new Thickness(85, 455, -200, -405);
                animationButton = FavouriteAnimationButton;
            }
            else if (srcButton.Name == "SkipButton")
            {
                margin = new Thickness(225, 455, -335, -405);
                animationButton = SkippedAnimationButton;
            }
            else
            {
                margin = new Thickness(500, 455, -335, -405);
                animationButton = DislikedAnimationButton;
            }

            Storyboard sb = FindResource("ClickAnimation") as Storyboard;
            animationButton.Visibility = Visibility.Visible;

            var toMargin = new Thickness
            {
                Left = margin.Left,
                Right = margin.Right,
                Top = margin.Top - 100,
                Bottom = margin.Bottom
            };
            var ta = new ThicknessAnimation
            {
                BeginTime = new TimeSpan(0),
                From = margin,
                To = toMargin,
                Duration = new Duration(TimeSpan.FromSeconds(0.55))
            };
            ta.SetValue(Storyboard.TargetNameProperty, "AnimationButton");
            Storyboard.SetTargetProperty(ta, new PropertyPath(MarginProperty));

            sb.Children.Add(ta);

            Storyboard.SetTarget(sb, animationButton);

            DispatcherTimer timer = new DispatcherTimer();
            timer.Tick += AnimationComplete;
            timer.Interval = TimeSpan.FromSeconds(0.15);

            sb.Begin();
            timer.Start();
        }

        private void AnimationComplete(object sender, EventArgs e)
        {
            browsingPage.AdvanceHouse();
            (sender as DispatcherTimer).Stop();
        }
    }
}