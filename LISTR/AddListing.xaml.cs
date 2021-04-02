using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace LISTR
{
    /// <summary>
    /// Interaction logic for AddListing.xaml
    /// </summary>
    public partial class AddListing : Page
    {
        private String defaultpath = "";
        private List<Image> images;

        public AddListing()
        {
            InitializeComponent();
        }

        private void SelectPhotos(object sender, MouseButtonEventArgs e)
        {
            if (defaultpath.Equals(""))
            {
                defaultpath = img1.Source.ToString();
            }

            Microsoft.Win32.OpenFileDialog dialogue = new Microsoft.Win32.OpenFileDialog();

            // set valid file extensions
            dialogue.DefaultExt = ".png";
            dialogue.Filter = "PNG Files (*.png)|*.png|JPEG Files (*.jpeg)|*.jpeg|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";

            // display the open file dialogue
            Nullable<bool> result = dialogue.ShowDialog();

            if (result == true)
            {
                string filename = dialogue.FileName; ;
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(filename);
                bitmap.EndInit();
                if (img1.Source.ToString().Contains("default.png"))
                {
                    img1.Source = bitmap;
                    img1_label.Visibility = Visibility.Visible;
                }
                else if (img2.Source.ToString().Contains("default.png"))
                {
                    img2.Source = bitmap;
                    img2_label.Visibility = Visibility.Visible;
                }
                else if (img3.Source.ToString().Contains("default.png"))
                {
                    img3.Source = bitmap;
                    img3_label.Visibility = Visibility.Visible;
                }
                else if (img4.Source.ToString().Contains("default.png"))
                {
                    img4.Source = bitmap;
                    img4_label.Visibility = Visibility.Visible;
                }
            }
        }

        private void img1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            img1.Source = new BitmapImage(new Uri(@defaultpath));
            img1_label.Visibility = Visibility.Hidden;
        }

        private void img2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            img2.Source = new BitmapImage(new Uri(@defaultpath));
            img2_label.Visibility = Visibility.Hidden;
        }

        private void img3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            img3.Source = new BitmapImage(new Uri(@defaultpath));
            img3_label.Visibility = Visibility.Hidden;
        }

        private void img4_MouseDown(object sender, MouseButtonEventArgs e)
        {
            img4.Source = new BitmapImage(new Uri(@defaultpath));
            img4_label.Visibility = Visibility.Hidden;
        }

        private void PostListing(object sender, RoutedEventArgs e)
        {
            House house = new House();
            house.Description = Description.Text;
            house.NumRooms = BedRooms.Text;
            house.NumBaths = BedRooms.Text;
        }
    }
}